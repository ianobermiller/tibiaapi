<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMap
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
        Me.chkFullLightOn = New System.Windows.Forms.CheckBox
        Me.chkNameSpyOn = New System.Windows.Forms.CheckBox
        Me.btnReplaceTrees = New System.Windows.Forms.Button
        Me.chkLevelSpyOn = New System.Windows.Forms.CheckBox
        Me.btnFloorDown = New System.Windows.Forms.Button
        Me.btnFloorUp = New System.Windows.Forms.Button
        Me.btnCheckMapTiles = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'chkFullLightOn
        '
        Me.chkFullLightOn.AutoSize = True
        Me.chkFullLightOn.Location = New System.Drawing.Point(12, 16)
        Me.chkFullLightOn.Name = "chkFullLightOn"
        Me.chkFullLightOn.Size = New System.Drawing.Size(85, 17)
        Me.chkFullLightOn.TabIndex = 0
        Me.chkFullLightOn.Text = "Full Light On"
        Me.chkFullLightOn.UseVisualStyleBackColor = True
        '
        'chkNameSpyOn
        '
        Me.chkNameSpyOn.AutoSize = True
        Me.chkNameSpyOn.Location = New System.Drawing.Point(12, 45)
        Me.chkNameSpyOn.Name = "chkNameSpyOn"
        Me.chkNameSpyOn.Size = New System.Drawing.Size(92, 17)
        Me.chkNameSpyOn.TabIndex = 1
        Me.chkNameSpyOn.Text = "Name Spy On"
        Me.chkNameSpyOn.UseVisualStyleBackColor = True
        '
        'btnReplaceTrees
        '
        Me.btnReplaceTrees.Location = New System.Drawing.Point(136, 41)
        Me.btnReplaceTrees.Name = "btnReplaceTrees"
        Me.btnReplaceTrees.Size = New System.Drawing.Size(144, 23)
        Me.btnReplaceTrees.TabIndex = 2
        Me.btnReplaceTrees.Text = "Replace Trees"
        Me.btnReplaceTrees.UseVisualStyleBackColor = True
        '
        'chkLevelSpyOn
        '
        Me.chkLevelSpyOn.AutoSize = True
        Me.chkLevelSpyOn.Location = New System.Drawing.Point(12, 87)
        Me.chkLevelSpyOn.Name = "chkLevelSpyOn"
        Me.chkLevelSpyOn.Size = New System.Drawing.Size(90, 17)
        Me.chkLevelSpyOn.TabIndex = 3
        Me.chkLevelSpyOn.Text = "Level Spy On"
        Me.chkLevelSpyOn.UseVisualStyleBackColor = True
        '
        'btnFloorDown
        '
        Me.btnFloorDown.Location = New System.Drawing.Point(211, 83)
        Me.btnFloorDown.Name = "btnFloorDown"
        Me.btnFloorDown.Size = New System.Drawing.Size(69, 23)
        Me.btnFloorDown.TabIndex = 4
        Me.btnFloorDown.Text = "Floor Down"
        Me.btnFloorDown.UseVisualStyleBackColor = True
        '
        'btnFloorUp
        '
        Me.btnFloorUp.Location = New System.Drawing.Point(136, 83)
        Me.btnFloorUp.Name = "btnFloorUp"
        Me.btnFloorUp.Size = New System.Drawing.Size(69, 23)
        Me.btnFloorUp.TabIndex = 5
        Me.btnFloorUp.Text = "Floor Up"
        Me.btnFloorUp.UseVisualStyleBackColor = True
        '
        'btnCheckMapTiles
        '
        Me.btnCheckMapTiles.Location = New System.Drawing.Point(136, 12)
        Me.btnCheckMapTiles.Name = "btnCheckMapTiles"
        Me.btnCheckMapTiles.Size = New System.Drawing.Size(144, 23)
        Me.btnCheckMapTiles.TabIndex = 8
        Me.btnCheckMapTiles.Text = "Check Map Tiles"
        Me.btnCheckMapTiles.UseVisualStyleBackColor = True
        '
        'frmMap
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 114)
        Me.Controls.Add(Me.btnCheckMapTiles)
        Me.Controls.Add(Me.btnFloorUp)
        Me.Controls.Add(Me.btnFloorDown)
        Me.Controls.Add(Me.chkLevelSpyOn)
        Me.Controls.Add(Me.btnReplaceTrees)
        Me.Controls.Add(Me.chkNameSpyOn)
        Me.Controls.Add(Me.chkFullLightOn)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmMap"
        Me.Text = "Map"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkFullLightOn As System.Windows.Forms.CheckBox
    Friend WithEvents chkNameSpyOn As System.Windows.Forms.CheckBox
    Friend WithEvents btnReplaceTrees As System.Windows.Forms.Button
    Friend WithEvents chkLevelSpyOn As System.Windows.Forms.CheckBox
    Friend WithEvents btnFloorDown As System.Windows.Forms.Button
    Friend WithEvents btnFloorUp As System.Windows.Forms.Button
    Friend WithEvents btnCheckMapTiles As System.Windows.Forms.Button
End Class
