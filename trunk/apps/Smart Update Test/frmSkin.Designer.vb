<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSkin
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
        Me.btnAddSkin = New System.Windows.Forms.Button
        Me.btnUpdateSkin = New System.Windows.Forms.Button
        Me.btnRemoveSkin = New System.Windows.Forms.Button
        Me.btnRemoveAllSkins = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnAddSkin
        '
        Me.btnAddSkin.Location = New System.Drawing.Point(12, 12)
        Me.btnAddSkin.Name = "btnAddSkin"
        Me.btnAddSkin.Size = New System.Drawing.Size(98, 23)
        Me.btnAddSkin.TabIndex = 0
        Me.btnAddSkin.Text = "Add Skin"
        Me.btnAddSkin.UseVisualStyleBackColor = True
        '
        'btnUpdateSkin
        '
        Me.btnUpdateSkin.Location = New System.Drawing.Point(116, 12)
        Me.btnUpdateSkin.Name = "btnUpdateSkin"
        Me.btnUpdateSkin.Size = New System.Drawing.Size(98, 23)
        Me.btnUpdateSkin.TabIndex = 1
        Me.btnUpdateSkin.Text = "Update Skin"
        Me.btnUpdateSkin.UseVisualStyleBackColor = True
        '
        'btnRemoveSkin
        '
        Me.btnRemoveSkin.Location = New System.Drawing.Point(220, 12)
        Me.btnRemoveSkin.Name = "btnRemoveSkin"
        Me.btnRemoveSkin.Size = New System.Drawing.Size(98, 23)
        Me.btnRemoveSkin.TabIndex = 2
        Me.btnRemoveSkin.Text = "Remove Skin"
        Me.btnRemoveSkin.UseVisualStyleBackColor = True
        '
        'btnRemoveAllSkins
        '
        Me.btnRemoveAllSkins.Location = New System.Drawing.Point(220, 41)
        Me.btnRemoveAllSkins.Name = "btnRemoveAllSkins"
        Me.btnRemoveAllSkins.Size = New System.Drawing.Size(98, 23)
        Me.btnRemoveAllSkins.TabIndex = 3
        Me.btnRemoveAllSkins.Text = "Remove All Skins"
        Me.btnRemoveAllSkins.UseVisualStyleBackColor = True
        '
        'frmSkin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(332, 76)
        Me.Controls.Add(Me.btnRemoveAllSkins)
        Me.Controls.Add(Me.btnRemoveSkin)
        Me.Controls.Add(Me.btnUpdateSkin)
        Me.Controls.Add(Me.btnAddSkin)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmSkin"
        Me.Text = "Skin"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnAddSkin As System.Windows.Forms.Button
    Friend WithEvents btnUpdateSkin As System.Windows.Forms.Button
    Friend WithEvents btnRemoveSkin As System.Windows.Forms.Button
    Friend WithEvents btnRemoveAllSkins As System.Windows.Forms.Button
End Class
