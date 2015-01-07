using FastColoredTextBoxNS;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace UVA_Arena.Elements
{
    public enum BuildRunType
    {
        BuildOnly,
        RunTest,
        BuildAndRun
    }

    public static class CodeCompiler
    {
        public static FileInfo CurrentProblem = null;
        public static long SelectedPNUM = -1;
        public static double RunTimeLimit = 3000;

        //
        // Report Progress
        //

        public static void ReportCompileStatus(string status, Style style = null)
        {
            Interactivity.codes.compilerOutput.BeginInvoke((MethodInvoker)delegate
            {
                //add message to compiler output
                if (status == null) Interactivity.codes.compilerOutput.AppendText(status);
                else Interactivity.codes.compilerOutput.AppendText(status, style);
            });
        }


        //
        // Build And Run Code
        //
        public static bool BuildAndRun(BuildRunType state, FileInfo currentProblem, long pnum = -1, double timelimit = 3000)
        {
            CurrentProblem = currentProblem;
            SelectedPNUM = pnum;
            RunTimeLimit = timelimit;

            try
            {
                //type of task to be done
                bool buildonly = (state == BuildRunType.BuildOnly);
                bool runtest = (state == BuildRunType.RunTest);

                //compile task
                CodeCompiler.StartBuildTask();

                //run builded file
                if (!buildonly)
                    CodeCompiler.StartRunTask(runtest);

                return true;
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "Codes");
                ReportCompileStatus(ex.Message + "\n", HighlightSyntax.CommentStyle);
                return false;
            }
        }

        private static void StartBuildTask()
        {
            //get extension of opened file                
            string ext = CurrentProblem.Extension.ToLower();

            //show building messege
            ReportCompileStatus("Building...\n\n", HighlightSyntax.CommentStyle);

            bool success = false;
            if (ext == ".c") success = C_Compile();
            else if (ext == ".cpp") success = CPP_Compile();
            else if (ext == ".java") success = Java_Compile();
            else throw new NotSupportedException("Language is not supported.");

            //show compilation result
            if (!success)
            {
                throw new ApplicationException("Compilation Failed " + CurrentProblem.FullName);
            }

            ReportCompileStatus("Successfully Compiled.\n", HighlightSyntax.CommentStyle);
        }

        private static void StartRunTask(bool runtest)
        {
            //get extension of opened file                
            string ext = CurrentProblem.Extension.ToLower();

            //show running message
            ReportCompileStatus("Running...\n", HighlightSyntax.CommentStyle);

            //run compiled code
            bool success = false;
            if (ext == ".c" || ext == ".cpp") success = C_CPP_Run(runtest);
            else if (ext == ".java") success = Java_Run(runtest);

            //show run result
            if (!success) throw new Exception("Process Failed.\n");

            ReportCompileStatus("Process Completed.\n", HighlightSyntax.CommentStyle);
        }


        //
        // C and C++ Run
        //
        private static bool C_CPP_Run(bool runtest)
        {
            //determine if custom input output is needed
            if (SelectedPNUM == -1) runtest = false;
            if (runtest && SelectedPNUM == -1) return false;

            //get exe file name
            string filename = Path.GetFileNameWithoutExtension(CurrentProblem.Name);
            string exec = Path.Combine(CurrentProblem.DirectoryName, filename + ".exe");

            //get argument parameters
            int runtime = (int)(RunTimeLimit * 1000);
            string input = Path.Combine(CurrentProblem.DirectoryName, "input.txt");
            string output = Path.Combine(CurrentProblem.DirectoryName, "output.txt");

            if (runtest)
            {
                //run process with predefined input
                string format = "\"{0}\" < \"{1}\" > \"{2}\"";
                string arguments = string.Format(format, exec, input, output);
                return ExecuteProcess(arguments, runtime, Path.GetFileName(exec));
            }
            else
            {
                //run process from a batch file
                string arguments = "\"" + exec + "\"";
                string title = Path.GetFileNameWithoutExtension(exec);
                return RunInBatch(arguments, title);
            }
        }

        //
        // Java Run
        //
        private static bool Java_Run(bool runtest)
        {
            //determine if custom input output is needed
            if (runtest && SelectedPNUM == -1) return false;
            if (SelectedPNUM == -1) runtest = false;

            //get compiler
            int runtime = (int)(RunTimeLimit * 1000);
            string exec = RegistryAccess.JDKCompilerPath;
            exec = Path.Combine(exec, "bin");
            exec = Path.Combine(exec, "java.exe");
            if (!File.Exists(exec))
                throw new FileNotFoundException("Invalid JDK path. Select one from settings.");

            //file names and directories
            string dir = CurrentProblem.DirectoryName;
            string input = Path.Combine(CurrentProblem.DirectoryName, "input.txt");
            string output = Path.Combine(CurrentProblem.DirectoryName, "output.txt");

            //format of the arguments 
            string format = "\"{0}\" -classpath \"{1}\" Main < \"{2}\" > \"{3}\" ";
            if (!runtest) format = "\"{0}\" -classpath \"{1}\" Main";
            string arguments = string.Format(format, exec, dir, input, output);

            if (runtest)
            {
                //run process with predefined input-output
                return ExecuteProcess(arguments, runtime, Path.GetFileName(exec));
            }
            {
                //run from a batch file
                string title = Path.GetFileNameWithoutExtension(CurrentProblem.Name);
                return RunInBatch(arguments, title);
            }
        }


        //
        // C Compiler
        // 
        private static bool C_Compile()
        {
            //file to compile
            string filename = CurrentProblem.FullName;

            //get compiler
            string compiler = RegistryAccess.MinGWCompilerPath;
            compiler = Path.Combine(compiler, "bin");
            compiler = Path.Combine(compiler, "mingw32-gcc.exe");
            if (!File.Exists(compiler))
                throw new FileNotFoundException("MinGW path is not valid. Select one from settings.");

            //filename
            string name = Path.GetFileNameWithoutExtension(filename);
            string exec = Path.Combine(Path.GetDirectoryName(filename), name + ".exe");

            //options
            string options = Properties.Settings.Default.CCompilerOptions;

            //run process
            string arguments = string.Format("\"{0}\" \"{1}\" {2} -o \"{3}", compiler, filename, options, exec);
            return ExecuteProcess(arguments);
        }

        //
        // C++ Compiler
        // 
        private static bool CPP_Compile()
        {
            //file to compile 
            string filename = CurrentProblem.FullName;

            //get compiler
            string compiler = RegistryAccess.MinGWCompilerPath;
            compiler = Path.Combine(compiler, "bin");
            compiler = Path.Combine(compiler, "mingw32-g++.exe");
            if (!File.Exists(compiler))
                throw new FileNotFoundException("MinGW path is not valid. Select one from settings.");

            //filename
            string name = Path.GetFileNameWithoutExtension(filename);
            string exec = Path.Combine(Path.GetDirectoryName(filename), name + ".exe");

            //options
            string options = Properties.Settings.Default.CPPCompilerOptions;

            //run process
            string arguments = string.Format("\"{0}\" \"{1}\" {2} -o \"{3}", compiler, filename, options, exec);
            return ExecuteProcess(arguments);
        }

        //
        // Java Compiler
        //
        private static bool Java_Compile()
        {
            //file to compile
            string filename = CurrentProblem.FullName;

            //get compiler
            string compiler = RegistryAccess.JDKCompilerPath;
            compiler = Path.Combine(compiler, "bin");
            compiler = Path.Combine(compiler, "javac.exe");
            if (!File.Exists(compiler))
                throw new FileNotFoundException("Invalid JDK path. Select one from settings.");

            //filename
            string dir = Path.GetDirectoryName(filename);

            //options
            string options = Properties.Settings.Default.JavaCompilerOptions;

            //run process
            string arguments = string.Format("\"{0}\" {1} -d \"{2}\" \"{3}\"", compiler, options, dir, filename);
            return ExecuteProcess(arguments);
        }

        //
        // Execution Process
        //

        private static void proc_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            string data = e.Data;
            if (data == null) return;

            //add message to compiler output
            data = data.Replace(CurrentProblem.FullName + ":", "");
            ReportCompileStatus(data.Trim() + "\n");
        }

        private static bool ExecuteProcess(string args, int timelim = -1, string procname = null)
        {
            //command prompt
            string cmd = Path.Combine(Environment.SystemDirectory, "cmd.exe");

            //prepare process
            Process proc = new Process();
            proc.ErrorDataReceived += proc_OutputDataReceived;
            proc.OutputDataReceived += proc_OutputDataReceived;
            proc.StartInfo = new ProcessStartInfo()
            {
                FileName = cmd,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = "/c \"" + args + "\""
            };

            //start process
            proc.Start();
            proc.BeginErrorReadLine();
            proc.BeginOutputReadLine();
            if (timelim == -1) proc.WaitForExit();
            else proc.WaitForExit(timelim);

            //generate ending report
            bool tle = !proc.HasExited; //check if time limit has exceeded
            if (tle) ForceKill(procname); //force kill tle tasks by process-name
            int exitcode = tle ? -1 : proc.ExitCode; //get exitcode
            double total = tle ? (timelim / 1000.0) : proc.TotalProcessorTime.TotalSeconds;
            string runtime = string.Format("Runtime = {0:0.000} sec.", total);
            string verdict = string.Format("Exit Code = {0} ({1}).", exitcode,
                (exitcode == -1 ? "tle" : (exitcode == 0 ? "Successfull" : "Error")));

            //show output 
            ReportCompileStatus("\n" + runtime + "\n", HighlightSyntax.CommentStyle);
            ReportCompileStatus(verdict + "\n", HighlightSyntax.CommentStyle);

            //return result
            proc.Dispose();
            return (exitcode == 0);
        }

        private static void ForceKill(string procname)
        {
            if (procname == null) return;

            //command prompt
            string cmd = Path.Combine(Environment.SystemDirectory, "cmd.exe");

            Process killer = new Process();

            //command prompt arguments                
            string taskkill = string.Format("/c \"taskkill /IM \"{0}\"\" /T /F", procname);
            killer.StartInfo = new ProcessStartInfo(cmd, taskkill)
            {
                //command prompt will be hidden
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            killer.Start();
            killer.WaitForExit();
            killer.Dispose();
        }

        private static bool RunInBatch(string args, string title)
        {
            //batch program
            string commands = 
                  "@{0}\n" +            //root                
                  "@cd \"{1}\"\n" +     //path
                  "@cls\n" +
                  "@title {2}\n" +      //title
                  "@{3}\n" +            //args
                  "@echo.\n" +
                  "@set /p exit=Press Enter to exit...%=%\n" +
                  "@exit";

            //get bat file
            string file = Path.GetTempFileName();            
            string bat = file + ".bat";
            File.Move(file, bat);

            string path = CurrentProblem.DirectoryName;
            string root = Path.GetPathRoot(path).Replace("\\", "");
   
            //write batch program to temporary file 
            File.WriteAllText(bat, string.Format(commands, root, path, title, args));

            //start batch file
            Process p = System.Diagnostics.Process.Start(bat);
            p.WaitForExit(); //wait till finish

            //delete batch file
            File.Delete(bat);

            //always task is completed
            return true;
        }

    }
}
