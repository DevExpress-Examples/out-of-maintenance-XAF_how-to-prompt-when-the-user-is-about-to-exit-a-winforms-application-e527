using System;
using System.Windows.Forms;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Win.SystemModule;

namespace WinSolution.Module.Win {
    public class PromptClosingLastMainWindowController : WindowController {
        private bool isEditModelExecuting = false;
        EditModelController editModelController;
        protected override void OnActivated() {
            base.OnActivated();
            editModelController = Window.GetController<EditModelController>();
            if(editModelController != null) {
                editModelController.EditModelAction.Executing += new CancelEventHandler(EditModelAction_Executing);
                editModelController.EditModelAction.ExecuteCompleted += new EventHandler<DevExpress.ExpressApp.Actions.ActionBaseEventArgs>(EditModelAction_ExecuteCompleted);
            }
        }
        private void EditModelAction_ExecuteCompleted(object sender, DevExpress.ExpressApp.Actions.ActionBaseEventArgs e) {
            isEditModelExecuting = false;
        }
        private void EditModelAction_Executing(object sender, CancelEventArgs e) {
            isEditModelExecuting = true;
        }
        protected override void OnWindowChanging(Window window) {
            base.OnWindowChanging(window);
            window.TemplateChanged += OnWindowTemplateChanged;
        }
        private void OnWindowClosing(Object sender, CancelEventArgs e) {
            FormClosingEventArgs ea = (FormClosingEventArgs)e;
            if(ea.CloseReason == CloseReason.UserClosing && Window.IsMain && !isEditModelExecuting &&
                ((WinShowViewStrategyBase)Application.ShowViewStrategy).Explorers.Count == 1 &&
                XtraMessageBox.Show("You are about to exit the application. Do you want to proceed?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No) {
                e.Cancel = true;
            }
        }
        private void OnWindowClosed(object sender, EventArgs e) {
            ((WinWindow)sender).Closing -= OnWindowClosing;
            ((WinWindow)sender).Closed -= OnWindowClosed;
        }
        private void OnWindowTemplateChanged(object sender, EventArgs e) {
            Window.TemplateChanged -= OnWindowTemplateChanged;
            ((WinWindow)Window).Closing += OnWindowClosing;
            ((WinWindow)Window).Closed += OnWindowClosed;
        }
        protected override void OnDeactivated() {
            if(editModelController != null) {
                editModelController.EditModelAction.Executing -= new CancelEventHandler(EditModelAction_Executing);
                editModelController.EditModelAction.ExecuteCompleted -= new EventHandler<DevExpress.ExpressApp.Actions.ActionBaseEventArgs>(EditModelAction_ExecuteCompleted);
                editModelController = null;
            }
            Window.TemplateChanged -= OnWindowTemplateChanged;
            ((WinWindow)Window).Closing += OnWindowClosing;
            ((WinWindow)Window).Closed += OnWindowClosed;
            base.OnDeactivated();
        }
    }
}