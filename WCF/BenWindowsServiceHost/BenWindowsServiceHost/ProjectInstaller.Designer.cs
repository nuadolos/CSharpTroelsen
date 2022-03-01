namespace BenWindowsServiceHost
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.BenProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.BenInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // BenProcessInstaller
            // 
            this.BenProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.BenProcessInstaller.Password = null;
            this.BenProcessInstaller.Username = null;
            // 
            // BenInstaller
            // 
            this.BenInstaller.Description = "Ben Talking now in the service Windows";
            this.BenInstaller.ServiceName = "BenService";
            this.BenInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.BenProcessInstaller,
            this.BenInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller BenProcessInstaller;
        private System.ServiceProcess.ServiceInstaller BenInstaller;
    }
}