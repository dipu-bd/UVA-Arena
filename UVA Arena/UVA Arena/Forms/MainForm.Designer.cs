namespace UVA_Arena
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabImageList = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setDefaultUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.submitFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.uDebugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.discussForumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.problemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.descriptionDownloaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.descriptionFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.backupDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreDatabseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.expandViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewCodeFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.codesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formatDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.editorSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setUpCompilerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePrecodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.submitCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.profilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.submissionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressTrackerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.basicInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.submissionOverTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.submissionPerVerdictrToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.submissionLanguagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rankCloudToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.worldRankToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlineHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.licenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customTabControl1 = new System.Windows.Forms.CustomTabControl();
            this.problemTab = new System.Windows.Forms.TabPage();
            this.codesTab = new System.Windows.Forms.TabPage();
            this.judgeStatusTab = new System.Windows.Forms.TabPage();
            this.profileTab = new System.Windows.Forms.TabPage();
            this.checkForUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1.SuspendLayout();
            this.customTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabImageList
            // 
            this.tabImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tabImageList.ImageStream")));
            this.tabImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.tabImageList.Images.SetKeyName(0, "code");
            this.tabImageList.Images.SetKeyName(1, "live_submission");
            this.tabImageList.Images.SetKeyName(2, "problems");
            this.tabImageList.Images.SetKeyName(3, "profile");
            this.tabImageList.Images.SetKeyName(4, "utility");
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LightBlue;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.problemsToolStripMenuItem,
            this.codesToolStripMenuItem,
            this.statusToolStripMenuItem,
            this.profilesToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(844, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setDefaultUserToolStripMenuItem,
            this.submitFormToolStripMenuItem,
            this.toolStripSeparator4,
            this.uDebugToolStripMenuItem,
            this.discussForumToolStripMenuItem,
            this.toolStripSeparator12,
            this.settingsToolStripMenuItem,
            this.loggerToolStripMenuItem,
            this.toolStripSeparator2,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.fileToolStripMenuItem.Text = "UVA Arena";
            // 
            // setDefaultUserToolStripMenuItem
            // 
            this.setDefaultUserToolStripMenuItem.Name = "setDefaultUserToolStripMenuItem";
            this.setDefaultUserToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.setDefaultUserToolStripMenuItem.Text = "Change Default User";
            this.setDefaultUserToolStripMenuItem.Click += new System.EventHandler(this.setDefaultUserToolStripMenuItem_Click);
            // 
            // submitFormToolStripMenuItem
            // 
            this.submitFormToolStripMenuItem.Image = global::UVA_Arena.Properties.Resources.submit;
            this.submitFormToolStripMenuItem.Name = "submitFormToolStripMenuItem";
            this.submitFormToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.submitFormToolStripMenuItem.Text = "Submit Form";
            this.submitFormToolStripMenuItem.Click += new System.EventHandler(this.submitFormToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(179, 6);
            // 
            // uDebugToolStripMenuItem
            // 
            this.uDebugToolStripMenuItem.Name = "uDebugToolStripMenuItem";
            this.uDebugToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.uDebugToolStripMenuItem.Text = "uDebug";
            this.uDebugToolStripMenuItem.Click += new System.EventHandler(this.uDebugToolStripMenuItem_Click);
            // 
            // discussForumToolStripMenuItem
            // 
            this.discussForumToolStripMenuItem.Name = "discussForumToolStripMenuItem";
            this.discussForumToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.discussForumToolStripMenuItem.Text = "Discuss Forum";
            this.discussForumToolStripMenuItem.Click += new System.EventHandler(this.discussForumToolStripMenuItem_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(179, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Image = global::UVA_Arena.Properties.Resources.tools;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // loggerToolStripMenuItem
            // 
            this.loggerToolStripMenuItem.Image = global::UVA_Arena.Properties.Resources.log;
            this.loggerToolStripMenuItem.Name = "loggerToolStripMenuItem";
            this.loggerToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.loggerToolStripMenuItem.Text = "Logger";
            this.loggerToolStripMenuItem.Click += new System.EventHandler(this.loggerToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(179, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.closeToolStripMenuItem.Text = "Exit";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // problemsToolStripMenuItem
            // 
            this.problemsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshDatabaseToolStripMenuItem,
            this.downloadDatabaseToolStripMenuItem,
            this.toolStripSeparator3,
            this.descriptionDownloaderToolStripMenuItem,
            this.descriptionFolderToolStripMenuItem,
            this.toolStripSeparator5,
            this.backupDatabaseToolStripMenuItem,
            this.restoreDatabseToolStripMenuItem,
            this.toolStripSeparator6,
            this.expandViewToolStripMenuItem,
            this.viewCodeFileToolStripMenuItem});
            this.problemsToolStripMenuItem.Name = "problemsToolStripMenuItem";
            this.problemsToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.problemsToolStripMenuItem.Text = "Problems";
            // 
            // refreshDatabaseToolStripMenuItem
            // 
            this.refreshDatabaseToolStripMenuItem.Image = global::UVA_Arena.Properties.Resources.reload;
            this.refreshDatabaseToolStripMenuItem.Name = "refreshDatabaseToolStripMenuItem";
            this.refreshDatabaseToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.refreshDatabaseToolStripMenuItem.Text = "Refresh Database";
            this.refreshDatabaseToolStripMenuItem.Click += new System.EventHandler(this.refreshDatabaseToolStripMenuItem_Click);
            // 
            // downloadDatabaseToolStripMenuItem
            // 
            this.downloadDatabaseToolStripMenuItem.Name = "downloadDatabaseToolStripMenuItem";
            this.downloadDatabaseToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.downloadDatabaseToolStripMenuItem.Text = "Download Database";
            this.downloadDatabaseToolStripMenuItem.Click += new System.EventHandler(this.downloadDatabaseToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(198, 6);
            // 
            // descriptionDownloaderToolStripMenuItem
            // 
            this.descriptionDownloaderToolStripMenuItem.Image = global::UVA_Arena.Properties.Resources.download;
            this.descriptionDownloaderToolStripMenuItem.Name = "descriptionDownloaderToolStripMenuItem";
            this.descriptionDownloaderToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.descriptionDownloaderToolStripMenuItem.Text = "Description Downloader";
            this.descriptionDownloaderToolStripMenuItem.Click += new System.EventHandler(this.descriptionDownloaderToolStripMenuItem_Click);
            // 
            // descriptionFolderToolStripMenuItem
            // 
            this.descriptionFolderToolStripMenuItem.Image = global::UVA_Arena.Properties.Resources.explorer;
            this.descriptionFolderToolStripMenuItem.Name = "descriptionFolderToolStripMenuItem";
            this.descriptionFolderToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.descriptionFolderToolStripMenuItem.Text = "Description Folder";
            this.descriptionFolderToolStripMenuItem.Click += new System.EventHandler(this.descriptionFolderToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(198, 6);
            // 
            // backupDatabaseToolStripMenuItem
            // 
            this.backupDatabaseToolStripMenuItem.Name = "backupDatabaseToolStripMenuItem";
            this.backupDatabaseToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.backupDatabaseToolStripMenuItem.Text = "Backup Database";
            this.backupDatabaseToolStripMenuItem.Click += new System.EventHandler(this.backupDatabaseToolStripMenuItem_Click);
            // 
            // restoreDatabseToolStripMenuItem
            // 
            this.restoreDatabseToolStripMenuItem.Name = "restoreDatabseToolStripMenuItem";
            this.restoreDatabseToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.restoreDatabseToolStripMenuItem.Text = "Restore Database";
            this.restoreDatabseToolStripMenuItem.Click += new System.EventHandler(this.restoreDatabseToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(198, 6);
            // 
            // expandViewToolStripMenuItem
            // 
            this.expandViewToolStripMenuItem.Image = global::UVA_Arena.Properties.Resources.expand;
            this.expandViewToolStripMenuItem.Name = "expandViewToolStripMenuItem";
            this.expandViewToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.expandViewToolStripMenuItem.Text = "Expand or Collapse";
            this.expandViewToolStripMenuItem.Click += new System.EventHandler(this.expandViewToolStripMenuItem_Click);
            // 
            // viewCodeFileToolStripMenuItem
            // 
            this.viewCodeFileToolStripMenuItem.Image = global::UVA_Arena.Properties.Resources.code;
            this.viewCodeFileToolStripMenuItem.Name = "viewCodeFileToolStripMenuItem";
            this.viewCodeFileToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.viewCodeFileToolStripMenuItem.Text = "View Code File";
            this.viewCodeFileToolStripMenuItem.Click += new System.EventHandler(this.viewCodeFileToolStripMenuItem_Click);
            // 
            // codesToolStripMenuItem
            // 
            this.codesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeDirectoryToolStripMenuItem,
            this.formatDirectoryToolStripMenuItem,
            this.toolStripSeparator7,
            this.refreshViewToolStripMenuItem,
            this.toolStripSeparator8,
            this.editorSettingToolStripMenuItem,
            this.setUpCompilerToolStripMenuItem,
            this.changePrecodesToolStripMenuItem,
            this.toolStripSeparator9,
            this.submitCodeToolStripMenuItem});
            this.codesToolStripMenuItem.Name = "codesToolStripMenuItem";
            this.codesToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.codesToolStripMenuItem.Text = "Codes";
            // 
            // changeDirectoryToolStripMenuItem
            // 
            this.changeDirectoryToolStripMenuItem.Image = global::UVA_Arena.Properties.Resources.folder;
            this.changeDirectoryToolStripMenuItem.Name = "changeDirectoryToolStripMenuItem";
            this.changeDirectoryToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.changeDirectoryToolStripMenuItem.Text = "Change Directory";
            this.changeDirectoryToolStripMenuItem.Click += new System.EventHandler(this.changeDirectoryToolStripMenuItem_Click);
            // 
            // formatDirectoryToolStripMenuItem
            // 
            this.formatDirectoryToolStripMenuItem.Name = "formatDirectoryToolStripMenuItem";
            this.formatDirectoryToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.formatDirectoryToolStripMenuItem.Text = "Format Directory";
            this.formatDirectoryToolStripMenuItem.Click += new System.EventHandler(this.formatDirectoryToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(163, 6);
            // 
            // refreshViewToolStripMenuItem
            // 
            this.refreshViewToolStripMenuItem.Image = global::UVA_Arena.Properties.Resources.reload;
            this.refreshViewToolStripMenuItem.Name = "refreshViewToolStripMenuItem";
            this.refreshViewToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.refreshViewToolStripMenuItem.Text = "Refresh View";
            this.refreshViewToolStripMenuItem.Click += new System.EventHandler(this.refreshViewToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(163, 6);
            // 
            // editorSettingToolStripMenuItem
            // 
            this.editorSettingToolStripMenuItem.Name = "editorSettingToolStripMenuItem";
            this.editorSettingToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.editorSettingToolStripMenuItem.Text = "Editor Settings";
            this.editorSettingToolStripMenuItem.Click += new System.EventHandler(this.editorSettingToolStripMenuItem_Click);
            // 
            // setUpCompilerToolStripMenuItem
            // 
            this.setUpCompilerToolStripMenuItem.Name = "setUpCompilerToolStripMenuItem";
            this.setUpCompilerToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.setUpCompilerToolStripMenuItem.Text = "Set up Compiler";
            this.setUpCompilerToolStripMenuItem.Click += new System.EventHandler(this.setUpCompilerToolStripMenuItem_Click);
            // 
            // changePrecodesToolStripMenuItem
            // 
            this.changePrecodesToolStripMenuItem.Image = global::UVA_Arena.Properties.Resources.code;
            this.changePrecodesToolStripMenuItem.Name = "changePrecodesToolStripMenuItem";
            this.changePrecodesToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.changePrecodesToolStripMenuItem.Text = "Change Precodes";
            this.changePrecodesToolStripMenuItem.Click += new System.EventHandler(this.changePrecodesToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(163, 6);
            // 
            // submitCodeToolStripMenuItem
            // 
            this.submitCodeToolStripMenuItem.Image = global::UVA_Arena.Properties.Resources.submit;
            this.submitCodeToolStripMenuItem.Name = "submitCodeToolStripMenuItem";
            this.submitCodeToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.submitCodeToolStripMenuItem.Text = "Submit Code";
            this.submitCodeToolStripMenuItem.Click += new System.EventHandler(this.submitCodeToolStripMenuItem_Click);
            // 
            // statusToolStripMenuItem
            // 
            this.statusToolStripMenuItem.Name = "statusToolStripMenuItem";
            this.statusToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.statusToolStripMenuItem.Text = "Status";
            // 
            // profilesToolStripMenuItem
            // 
            this.profilesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addUserToolStripMenuItem,
            this.toolStripSeparator10,
            this.submissionsToolStripMenuItem,
            this.progressTrackerToolStripMenuItem,
            this.worldRankToolStripMenuItem,
            this.compareToolStripMenuItem});
            this.profilesToolStripMenuItem.Name = "profilesToolStripMenuItem";
            this.profilesToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.profilesToolStripMenuItem.Text = "Profiles";
            // 
            // addUserToolStripMenuItem
            // 
            this.addUserToolStripMenuItem.Name = "addUserToolStripMenuItem";
            this.addUserToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.addUserToolStripMenuItem.Text = "Add User";
            this.addUserToolStripMenuItem.Click += new System.EventHandler(this.addUserToolStripMenuItem_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(158, 6);
            // 
            // submissionsToolStripMenuItem
            // 
            this.submissionsToolStripMenuItem.Name = "submissionsToolStripMenuItem";
            this.submissionsToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.submissionsToolStripMenuItem.Text = "Submissions";
            // 
            // progressTrackerToolStripMenuItem
            // 
            this.progressTrackerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.basicInfoToolStripMenuItem,
            this.submissionOverTimeToolStripMenuItem,
            this.submissionPerVerdictrToolStripMenuItem,
            this.submissionLanguagesToolStripMenuItem,
            this.rankCloudToolStripMenuItem});
            this.progressTrackerToolStripMenuItem.Name = "progressTrackerToolStripMenuItem";
            this.progressTrackerToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.progressTrackerToolStripMenuItem.Text = "Progress Tracker";
            // 
            // basicInfoToolStripMenuItem
            // 
            this.basicInfoToolStripMenuItem.Name = "basicInfoToolStripMenuItem";
            this.basicInfoToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.basicInfoToolStripMenuItem.Text = "Basic Info";
            this.basicInfoToolStripMenuItem.Click += new System.EventHandler(this.basicInfoToolStripMenuItem_Click);
            // 
            // submissionOverTimeToolStripMenuItem
            // 
            this.submissionOverTimeToolStripMenuItem.Name = "submissionOverTimeToolStripMenuItem";
            this.submissionOverTimeToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.submissionOverTimeToolStripMenuItem.Text = "Submission Over Time";
            this.submissionOverTimeToolStripMenuItem.Click += new System.EventHandler(this.submissionOverTimeToolStripMenuItem_Click);
            // 
            // submissionPerVerdictrToolStripMenuItem
            // 
            this.submissionPerVerdictrToolStripMenuItem.Name = "submissionPerVerdictrToolStripMenuItem";
            this.submissionPerVerdictrToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.submissionPerVerdictrToolStripMenuItem.Text = "Submission Per Verdict";
            this.submissionPerVerdictrToolStripMenuItem.Click += new System.EventHandler(this.submissionPerVerdictrToolStripMenuItem_Click);
            // 
            // submissionLanguagesToolStripMenuItem
            // 
            this.submissionLanguagesToolStripMenuItem.Name = "submissionLanguagesToolStripMenuItem";
            this.submissionLanguagesToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.submissionLanguagesToolStripMenuItem.Text = "Submission Languages";
            this.submissionLanguagesToolStripMenuItem.Click += new System.EventHandler(this.submissionLanguagesToolStripMenuItem_Click);
            // 
            // rankCloudToolStripMenuItem
            // 
            this.rankCloudToolStripMenuItem.Name = "rankCloudToolStripMenuItem";
            this.rankCloudToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.rankCloudToolStripMenuItem.Text = "Rank Cloud";
            this.rankCloudToolStripMenuItem.Click += new System.EventHandler(this.rankCloudToolStripMenuItem_Click);
            // 
            // worldRankToolStripMenuItem
            // 
            this.worldRankToolStripMenuItem.Name = "worldRankToolStripMenuItem";
            this.worldRankToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.worldRankToolStripMenuItem.Text = "World Rank";
            this.worldRankToolStripMenuItem.Click += new System.EventHandler(this.worldRankToolStripMenuItem_Click);
            // 
            // compareToolStripMenuItem
            // 
            this.compareToolStripMenuItem.Name = "compareToolStripMenuItem";
            this.compareToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.compareToolStripMenuItem.Text = "Compare";
            this.compareToolStripMenuItem.Click += new System.EventHandler(this.compareToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlineHelpToolStripMenuItem,
            this.toolStripSeparator1,
            this.licenceToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.toolStripSeparator11,
            this.checkForUpdateToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // onlineHelpToolStripMenuItem
            // 
            this.onlineHelpToolStripMenuItem.Image = global::UVA_Arena.Properties.Resources.web;
            this.onlineHelpToolStripMenuItem.Name = "onlineHelpToolStripMenuItem";
            this.onlineHelpToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.onlineHelpToolStripMenuItem.Text = "Online Help";
            this.onlineHelpToolStripMenuItem.Click += new System.EventHandler(this.onlineHelpToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(162, 6);
            // 
            // licenceToolStripMenuItem
            // 
            this.licenceToolStripMenuItem.Name = "licenceToolStripMenuItem";
            this.licenceToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.licenceToolStripMenuItem.Text = "Licence";
            this.licenceToolStripMenuItem.Click += new System.EventHandler(this.licenceToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = global::UVA_Arena.Properties.Resources.help;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // customTabControl1
            // 
            this.customTabControl1.Controls.Add(this.problemTab);
            this.customTabControl1.Controls.Add(this.codesTab);
            this.customTabControl1.Controls.Add(this.judgeStatusTab);
            this.customTabControl1.Controls.Add(this.profileTab);
            this.customTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customTabControl1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTabControl1.ImageList = this.tabImageList;
            this.customTabControl1.Location = new System.Drawing.Point(0, 24);
            this.customTabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.customTabControl1.Name = "customTabControl1";
            this.customTabControl1.Overlap = 6;
            this.customTabControl1.Padding = new System.Drawing.Point(30, 7);
            this.customTabControl1.SelectedIndex = 0;
            this.customTabControl1.Size = new System.Drawing.Size(844, 418);
            this.customTabControl1.TabIndex = 0;
            // 
            // problemTab
            // 
            this.problemTab.BackColor = System.Drawing.Color.PaleTurquoise;
            this.problemTab.ImageKey = "problems";
            this.problemTab.Location = new System.Drawing.Point(4, 33);
            this.problemTab.Name = "problemTab";
            this.problemTab.Size = new System.Drawing.Size(836, 381);
            this.problemTab.TabIndex = 1;
            this.problemTab.Text = "PROBLEMS";
            // 
            // codesTab
            // 
            this.codesTab.BackColor = System.Drawing.Color.PaleTurquoise;
            this.codesTab.ImageKey = "code";
            this.codesTab.Location = new System.Drawing.Point(4, 33);
            this.codesTab.Name = "codesTab";
            this.codesTab.Size = new System.Drawing.Size(836, 381);
            this.codesTab.TabIndex = 3;
            this.codesTab.Text = "CODES";
            // 
            // judgeStatusTab
            // 
            this.judgeStatusTab.BackColor = System.Drawing.Color.PaleTurquoise;
            this.judgeStatusTab.ImageKey = "live_submission";
            this.judgeStatusTab.Location = new System.Drawing.Point(4, 33);
            this.judgeStatusTab.Name = "judgeStatusTab";
            this.judgeStatusTab.Size = new System.Drawing.Size(836, 381);
            this.judgeStatusTab.TabIndex = 6;
            this.judgeStatusTab.Text = "STATUS";
            // 
            // profileTab
            // 
            this.profileTab.BackColor = System.Drawing.Color.PaleTurquoise;
            this.profileTab.ImageKey = "profile";
            this.profileTab.Location = new System.Drawing.Point(4, 33);
            this.profileTab.Name = "profileTab";
            this.profileTab.Size = new System.Drawing.Size(836, 381);
            this.profileTab.TabIndex = 2;
            this.profileTab.Text = "PROFILES";
            // 
            // checkForUpdateToolStripMenuItem
            // 
            this.checkForUpdateToolStripMenuItem.Name = "checkForUpdateToolStripMenuItem";
            this.checkForUpdateToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.checkForUpdateToolStripMenuItem.Text = "Check for update";
            this.checkForUpdateToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdateToolStripMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(162, 6);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(844, 442);
            this.Controls.Add(this.customTabControl1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Tag = "UVA Arena [{0} : {1}]";
            this.Text = "UVA Arena";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.customTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList tabImageList;
        public System.Windows.Forms.CustomTabControl customTabControl1;
        public System.Windows.Forms.TabPage problemTab;
        public System.Windows.Forms.TabPage profileTab;
        public System.Windows.Forms.TabPage codesTab;
        public System.Windows.Forms.TabPage judgeStatusTab;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loggerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem problemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem codesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem profilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onlineHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setDefaultUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem downloadDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem descriptionDownloaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem backupDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreDatabseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem licenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expandViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formatDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem refreshViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem setUpCompilerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePrecodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem submitCodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem submitFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewCodeFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem descriptionFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem submissionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem progressTrackerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem basicInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem submissionOverTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem submissionPerVerdictrToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem submissionLanguagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uDebugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem discussForumToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripMenuItem worldRankToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compareToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rankCloudToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editorSettingToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdateToolStripMenuItem;
          
    }
}

