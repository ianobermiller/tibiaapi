<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPackets
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
        Me.btnServerMemory = New System.Windows.Forms.Button
        Me.btnClientMemory = New System.Windows.Forms.Button
        Me.btnClientHookProxy = New System.Windows.Forms.Button
        Me.btnServerHookProxy = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnServerMemory
        '
        Me.btnServerMemory.Location = New System.Drawing.Point(12, 41)
        Me.btnServerMemory.Name = "btnServerMemory"
        Me.btnServerMemory.Size = New System.Drawing.Size(197, 23)
        Me.btnServerMemory.TabIndex = 0
        Me.btnServerMemory.Text = "Send Packet To Server By Memory"
        Me.btnServerMemory.UseVisualStyleBackColor = True
        '
        'btnClientMemory
        '
        Me.btnClientMemory.Location = New System.Drawing.Point(12, 12)
        Me.btnClientMemory.Name = "btnClientMemory"
        Me.btnClientMemory.Size = New System.Drawing.Size(197, 23)
        Me.btnClientMemory.TabIndex = 1
        Me.btnClientMemory.Text = "Send Packet To Client By Memory"
        Me.btnClientMemory.UseVisualStyleBackColor = True
        '
        'btnClientHookProxy
        '
        Me.btnClientHookProxy.Location = New System.Drawing.Point(12, 87)
        Me.btnClientHookProxy.Name = "btnClientHookProxy"
        Me.btnClientHookProxy.Size = New System.Drawing.Size(197, 23)
        Me.btnClientHookProxy.TabIndex = 2
        Me.btnClientHookProxy.Text = "Send Packet To Client By HookProxy" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.btnClientHookProxy.UseVisualStyleBackColor = True
        '
        'btnServerHookProxy
        '
        Me.btnServerHookProxy.Location = New System.Drawing.Point(12, 116)
        Me.btnServerHookProxy.Name = "btnServerHookProxy"
        Me.btnServerHookProxy.Size = New System.Drawing.Size(197, 23)
        Me.btnServerHookProxy.TabIndex = 3
        Me.btnServerHookProxy.Text = "Send Packet To Server By HookProxy" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.btnServerHookProxy.UseVisualStyleBackColor = True
        '
        'frmPackets
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(221, 151)
        Me.Controls.Add(Me.btnServerHookProxy)
        Me.Controls.Add(Me.btnClientHookProxy)
        Me.Controls.Add(Me.btnClientMemory)
        Me.Controls.Add(Me.btnServerMemory)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmPackets"
        Me.Text = "Packets"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnServerMemory As System.Windows.Forms.Button
    Friend WithEvents btnClientMemory As System.Windows.Forms.Button
    Friend WithEvents btnClientHookProxy As System.Windows.Forms.Button
    Friend WithEvents btnServerHookProxy As System.Windows.Forms.Button
End Class
