namespace UVA_Arena.Structures
{
    public enum Language
    {
        Other = 0,
        C = 1,
        Java = 2,
        CPP = 3,
        Pascal = 4,
        CPP11 = 5
    }

    public enum Verdict
    {
        SubError = 10,
        CannotBeJudge = 15,
        InQueue = 20,
        CompileError = 30,
        RestrictedFunction = 35,
        RuntimeError = 40,
        OutputLimit = 45,
        TimLimit = 50,
        MemoryLimit = 60,
        WrongAnswer = 70,
        PresentationError = 80,
        Accepted = 90
    }

    public class SubmissionMessage
    {
        public long sid { get; set; }
        public long uid { get; set; }
        public long pid { get; set; }
        public long ver { get; set; }
        public long lan { get; set; }
        public long run { get; set; }
        public long mem { get; set; }
        public long rank { get; set; }
        public long sbt { get; set; }
        public string name { get; set; }
        public string uname { get; set; }

        public long pnum { get { return LocalDatabase.GetNumber(pid); } }
        public string ptitle { get { return LocalDatabase.GetTitle(pnum); } }

        public SubmissionMessage() { }
    }

    public class JudgeStatus
    {
        public JudgeStatus() { }

        public long id { get; set; }
        public string type { get; set; }
        public SubmissionMessage msg { get; set; }

        public long sid { get { return msg.sid; } set { msg.sid = value; } }
        public long uid { get { return msg.uid; } set { msg.uid = value; } }
        public long pid { get { return msg.pid; } set { msg.pid = value; } }
        public long run { get { return msg.run; } set { msg.run = value; } }
        public long mem { get { return msg.mem; } set { msg.mem = value; } }
        public long rank { get { return msg.rank; } set { msg.rank = value; } }
        public long sbt { get { return msg.sbt; } set { msg.sbt = value; } }
        public string name { get { return msg.name; } set { msg.name = value; } }
        public string uname { get { return msg.uname; } set { msg.uname = value; } }
        public long pnum { get { return msg.pnum; } }
        public string ptitle { get { return msg.ptitle; } }
        public Verdict ver
        {
            get { return (Verdict)msg.ver; }
            set { msg.ver = (int)value; }
        }
        public Language lan
        {
            get { return (Language)msg.lan; }
            set { msg.lan = (int)value; }
        }
    }
}