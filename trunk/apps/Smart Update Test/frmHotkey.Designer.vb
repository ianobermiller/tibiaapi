<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHotkey
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lvHotkeys = New System.Windows.Forms.ListView
        Me.chSendAutomatically = New System.Windows.Forms.ColumnHeader
        Me.chText = New System.Windows.Forms.ColumnHeader
        Me.chObjectId = New System.Windows.Forms.ColumnHeader
        Me.chObjectUseType = New System.Windows.Forms.ColumnHeader
        Me.chShortcut = New System.Windows.Forms.ColumnHeader
        Me.btnGetHotkeys = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lvHotkeys
        '
        Me.lvHotkeys.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chSendAutomatically, Me.chText, Me.chObjectId, Me.chObjectUseType, Me.chShortcut})
        Me.lvHotkeys.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvHotkeys.Location = New System.Drawing.Point(12, 12)
        Me.lvHotkeys.MultiSelect = False
        Me.lvHotkeys.Name = "lvHotkeys"
        Me.lvHotkeys.Size = New System.Drawing.Size(308, 97)
        Me.lvHotkeys.TabIndex = 0
        Me.lvHotkeys.UseCompatibleStateImageBehavior = False
        Me.lvHotkeys.View = System.Windows.Forms.View.Details
        '
        'chSendAutomatically
        '
        Me.chSendAutomatically.Text = "Send Automatically"
        '
        'chText
        '
        Me.chText.Text = "Text"
        '
        'chObjectId
        '
        Me.chObjectId.Text = "Object Id"
        '
        'chObjectUseType
        '
        Me.chObjectUseType.Text = "Object Use Type"
        '
        'chShortcut
        '
        Me.chShortcut.Text = "Shortcut"
        '
        'btnGetHotkeys
        '
        Me.btnGetHotkeys.Location = New System.Drawing.Point(245, 115)
        Me.btnGetHotkeys.Name = "btnGetHotkeys"
        Me.btnGetHotkeys.Size = New System.Drawing.Size(75, 23)
        Me.btnGetHotkeys.TabIndex = 1
        Me.btnGetHotkeys.Text = "Get Hotkeys"
        Me.btnGetHotkeys.UseVisualStyleBackColor = True
        '
        'frmHotkey
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(332, 144)
        Me.Controls.Add(Me.btnGetHotkeys)
        Me.Controls.Add(Me.lvHotkeys)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmHotkey"
        Me.Text = "Hotkey"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lvHotkeys As System.Windows.Forms.ListView
    Friend WithEvents chSendAutomatically As System.Windows.Forms.ColumnHeader
    Friend WithEvents chText As System.Windows.Forms.ColumnHeader
    Friend WithEvents chObjectId As System.Windows.Forms.ColumnHeader
    Friend WithEvents chObjectUseType As System.Windows.Forms.ColumnHeader
    Friend WithEvents chShortcut As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnGetHotkeys As System.Windows.Forms.Button
End Class
