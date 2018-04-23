Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports DevExpress.XtraEditors
Imports DevExpress.ExpressApp.Win
Imports DevExpress.ExpressApp.Win.SystemModule

Namespace WinSolution.Module.Win
	Public Class PromptClosingLastMainWindowController
		Inherits WindowController
		Private isEditModelExecuting As Boolean = False
		Private editModelController As EditModelController
		Protected Overrides Sub OnActivated()
			MyBase.OnActivated()
			editModelController = Window.GetController(Of EditModelController)()
			If editModelController IsNot Nothing Then
				AddHandler editModelController.EditModelAction.Executing, AddressOf EditModelAction_Executing
				AddHandler editModelController.EditModelAction.ExecuteCompleted, AddressOf EditModelAction_ExecuteCompleted
			End If
		End Sub
		Private Sub EditModelAction_ExecuteCompleted(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.Actions.ActionBaseEventArgs)
			isEditModelExecuting = False
		End Sub
		Private Sub EditModelAction_Executing(ByVal sender As Object, ByVal e As CancelEventArgs)
			isEditModelExecuting = True
		End Sub
		Protected Overrides Sub OnWindowChanging(ByVal window As Window)
			MyBase.OnWindowChanging(window)
			AddHandler window.TemplateChanged, AddressOf OnWindowTemplateChanged
		End Sub
		Private Sub OnWindowClosing(ByVal sender As Object, ByVal e As CancelEventArgs)
			Dim ea As FormClosingEventArgs = CType(e, FormClosingEventArgs)
			If ea.CloseReason = CloseReason.UserClosing AndAlso Window.IsMain AndAlso (Not isEditModelExecuting) AndAlso (CType(Application.ShowViewStrategy, WinShowViewStrategyBase)).Explorers.Count = 1 AndAlso XtraMessageBox.Show("You are about to exit the application. Do you want to proceed?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.No Then
				e.Cancel = True
			End If
		End Sub
		Private Sub OnWindowClosed(ByVal sender As Object, ByVal e As EventArgs)
			RemoveHandler (CType(sender, WinWindow)).Closing, AddressOf OnWindowClosing
			RemoveHandler (CType(sender, WinWindow)).Closed, AddressOf OnWindowClosed
		End Sub
		Private Sub OnWindowTemplateChanged(ByVal sender As Object, ByVal e As EventArgs)
			RemoveHandler Window.TemplateChanged, AddressOf OnWindowTemplateChanged
			AddHandler (CType(Window, WinWindow)).Closing, AddressOf OnWindowClosing
			AddHandler (CType(Window, WinWindow)).Closed, AddressOf OnWindowClosed
		End Sub
		Protected Overrides Sub OnDeactivated()
			If editModelController IsNot Nothing Then
				RemoveHandler editModelController.EditModelAction.Executing, AddressOf EditModelAction_Executing
				RemoveHandler editModelController.EditModelAction.ExecuteCompleted, AddressOf EditModelAction_ExecuteCompleted
				editModelController = Nothing
			End If
			RemoveHandler Window.TemplateChanged, AddressOf OnWindowTemplateChanged
			AddHandler (CType(Window, WinWindow)).Closing, AddressOf OnWindowClosing
			AddHandler (CType(Window, WinWindow)).Closed, AddressOf OnWindowClosed
			MyBase.OnDeactivated()
		End Sub
	End Class
End Namespace