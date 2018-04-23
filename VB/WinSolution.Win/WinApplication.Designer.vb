Imports Microsoft.VisualBasic
Imports System
Namespace WinSolution.Win
	Partial Public Class WinSolutionWindowsFormsApplication
		''' <summary> 
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary> 
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Component Designer generated code"

		''' <summary> 
		''' Required method for Designer support - do not modify 
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
			Me.module2 = New DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule()
			Me.module4 = New WinSolution.Module.Win.WinSolutionWindowsFormsModule()
			Me.scriptRecorderWindowsFormsModule1 = New DevExpress.ExpressApp.ScriptRecorder.Win.ScriptRecorderWindowsFormsModule()
			Me.scriptRecorderModuleBase1 = New DevExpress.ExpressApp.ScriptRecorder.ScriptRecorderModuleBase()
			CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
			' 
			' WinSolutionWindowsFormsApplication
			' 
			Me.ApplicationName = "WinSolution"
			Me.Modules.Add(Me.module1)
			Me.Modules.Add(Me.module2)
			Me.Modules.Add(Me.module4)
			Me.Modules.Add(Me.scriptRecorderModuleBase1)
			Me.Modules.Add(Me.scriptRecorderWindowsFormsModule1)
'			Me.DatabaseVersionMismatch += New System.EventHandler(Of DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs)(Me.WinSolutionWindowsFormsApplication_DatabaseVersionMismatch);
			CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

		End Sub

		#End Region

		Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
		Private module2 As DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule
		Private module4 As WinSolution.Module.Win.WinSolutionWindowsFormsModule
		Private scriptRecorderWindowsFormsModule1 As DevExpress.ExpressApp.ScriptRecorder.Win.ScriptRecorderWindowsFormsModule
		Private scriptRecorderModuleBase1 As DevExpress.ExpressApp.ScriptRecorder.ScriptRecorderModuleBase
	End Class
End Namespace
