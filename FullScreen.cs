// class sourced from : https://stackoverflow.com/questions/505167/how-do-i-make-a-winforms-app-go-full-screen
using VideoLOB;

namespace ALTViewer
{
    class FullScreen
    {
        Form TargetForm;

        FormWindowState PreviousWindowState;

        public Boolean enabled;
        public FullScreen(Form targetForm)
        {
            TargetForm = targetForm;
            TargetForm.KeyPreview = true;
        }
        public void Toggle()
        {
            if (TargetForm.WindowState == FormWindowState.Maximized)
            {
                Leave();
                enabled = false;
            }
            else
            {
                Enter();
                enabled = true;
            }
        }

        public void Enter()
        {
            if (TargetForm.WindowState != FormWindowState.Maximized)
            {
                PreviousWindowState = TargetForm.WindowState;
                TargetForm.WindowState = FormWindowState.Normal;
                TargetForm.FormBorderStyle = FormBorderStyle.None;
                TargetForm.WindowState = FormWindowState.Maximized;
            }
        }

        public void Leave()
        {
            TargetForm.FormBorderStyle = FormBorderStyle.Sizable;
            TargetForm.WindowState = PreviousWindowState;
        }
    }
}
