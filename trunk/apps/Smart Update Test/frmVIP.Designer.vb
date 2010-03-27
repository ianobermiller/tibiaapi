<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVIP
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
        Me.lvVIP = New System.Windows.Forms.ListView
        Me.chId = New System.Windows.Forms.ColumnHeader
        Me.chName = New System.Windows.Forms.ColumnHeader
        Me.chStatus = New System.Windows.Forms.ColumnHeader
        Me.chIcon = New System.Windows.Forms.ColumnHeader
        Me.btnGetVIP = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lvVIP
        '
        Me.lvVIP.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chId, Me.chName, Me.chStatus, Me.chIcon})
        Me.lvVIP.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvVIP.Location = New System.Drawing.Point(12, 12)
        Me.lvVIP.MultiSelect = False
        Me.lvVIP.Name = "lvVIP"
        Me.lvVIP.Size = New System.Drawing.Size(328, 97)
        Me.lvVIP.TabIndex = 0
        Me.lvVIP.UseCompatibleStateImageBehavior = False
        Me.lvVIP.View = System.Windows.Forms.View.Details
        '
        'chId
        '
        Me.chId.Text = "Id"
        '
        'chName
        '
        Me.chName.Text = "Name"
        '
        'chStatus
        '
        Me.chStatus.Text = "Status"
        '
        'chIcon
        '
        Me.chIcon.Text = "Icon"
        '
        'btnGetVIP
        '
        Me.btnGetVIP.Location = New System.Drawing.Point(265, 115)
        Me.btnGetVIP.Name = "btnGetVIP"
        Me.btnGetVIP.Size = New System.Drawing.Size(75, 23)
        Me.btnGetVIP.TabIndex = 1
        Me.btnGetVIP.Text = "Get VIP"
        Me.btnGetVIP.UseVisualStyleBackColor = True
        '
        'frmVIP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(352, 145)
        Me.Controls.Add(Me.btnGetVIP)
        Me.Controls.Add(Me.lvVIP)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmVIP"
        Me.Text = "VIP"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lvVIP As System.Windows.Forms.ListView
    Friend WithEvents chId As System.Windows.Forms.ColumnHeader
    Friend WithEvents chName As System.Windows.Forms.ColumnHeader
    Friend WithEvents chStatus As System.Windows.Forms.ColumnHeader
    Friend WithEvents chIcon As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnGetVIP As System.Windows.Forms.Button
End Class
