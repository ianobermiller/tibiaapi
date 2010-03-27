<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIcon
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
        Me.btnAddGoldCoinIcon = New System.Windows.Forms.Button
        Me.btnUpdateGoldCoinIcon = New System.Windows.Forms.Button
        Me.btnAddParcelIcon = New System.Windows.Forms.Button
        Me.btnUpdateParcelIcon = New System.Windows.Forms.Button
        Me.btnRemoveGoldCoinIcon = New System.Windows.Forms.Button
        Me.btnRemoveParcelIcon = New System.Windows.Forms.Button
        Me.btnRemoveAll = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnAddGoldCoinIcon
        '
        Me.btnAddGoldCoinIcon.Location = New System.Drawing.Point(12, 12)
        Me.btnAddGoldCoinIcon.Name = "btnAddGoldCoinIcon"
        Me.btnAddGoldCoinIcon.Size = New System.Drawing.Size(131, 23)
        Me.btnAddGoldCoinIcon.TabIndex = 0
        Me.btnAddGoldCoinIcon.Text = "Add Gold Coin Icon"
        Me.btnAddGoldCoinIcon.UseVisualStyleBackColor = True
        '
        'btnUpdateGoldCoinIcon
        '
        Me.btnUpdateGoldCoinIcon.Location = New System.Drawing.Point(149, 12)
        Me.btnUpdateGoldCoinIcon.Name = "btnUpdateGoldCoinIcon"
        Me.btnUpdateGoldCoinIcon.Size = New System.Drawing.Size(131, 23)
        Me.btnUpdateGoldCoinIcon.TabIndex = 1
        Me.btnUpdateGoldCoinIcon.Text = "Update Gold Coin Icon"
        Me.btnUpdateGoldCoinIcon.UseVisualStyleBackColor = True
        '
        'btnAddParcelIcon
        '
        Me.btnAddParcelIcon.Location = New System.Drawing.Point(12, 41)
        Me.btnAddParcelIcon.Name = "btnAddParcelIcon"
        Me.btnAddParcelIcon.Size = New System.Drawing.Size(131, 23)
        Me.btnAddParcelIcon.TabIndex = 2
        Me.btnAddParcelIcon.Text = "Add Parcel Icon"
        Me.btnAddParcelIcon.UseVisualStyleBackColor = True
        '
        'btnUpdateParcelIcon
        '
        Me.btnUpdateParcelIcon.Location = New System.Drawing.Point(149, 41)
        Me.btnUpdateParcelIcon.Name = "btnUpdateParcelIcon"
        Me.btnUpdateParcelIcon.Size = New System.Drawing.Size(131, 23)
        Me.btnUpdateParcelIcon.TabIndex = 3
        Me.btnUpdateParcelIcon.Text = "Update Parcel Icon"
        Me.btnUpdateParcelIcon.UseVisualStyleBackColor = True
        '
        'btnRemoveGoldCoinIcon
        '
        Me.btnRemoveGoldCoinIcon.Location = New System.Drawing.Point(286, 12)
        Me.btnRemoveGoldCoinIcon.Name = "btnRemoveGoldCoinIcon"
        Me.btnRemoveGoldCoinIcon.Size = New System.Drawing.Size(131, 23)
        Me.btnRemoveGoldCoinIcon.TabIndex = 4
        Me.btnRemoveGoldCoinIcon.Text = "Remove Gold Coin Icon"
        Me.btnRemoveGoldCoinIcon.UseVisualStyleBackColor = True
        '
        'btnRemoveParcelIcon
        '
        Me.btnRemoveParcelIcon.Location = New System.Drawing.Point(286, 41)
        Me.btnRemoveParcelIcon.Name = "btnRemoveParcelIcon"
        Me.btnRemoveParcelIcon.Size = New System.Drawing.Size(131, 23)
        Me.btnRemoveParcelIcon.TabIndex = 5
        Me.btnRemoveParcelIcon.Text = "Remove Parcel Icon"
        Me.btnRemoveParcelIcon.UseVisualStyleBackColor = True
        '
        'btnRemoveAll
        '
        Me.btnRemoveAll.Location = New System.Drawing.Point(286, 70)
        Me.btnRemoveAll.Name = "btnRemoveAll"
        Me.btnRemoveAll.Size = New System.Drawing.Size(131, 23)
        Me.btnRemoveAll.TabIndex = 6
        Me.btnRemoveAll.Text = "Remove All"
        Me.btnRemoveAll.UseVisualStyleBackColor = True
        '
        'frmIcon
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(429, 100)
        Me.Controls.Add(Me.btnRemoveAll)
        Me.Controls.Add(Me.btnRemoveParcelIcon)
        Me.Controls.Add(Me.btnRemoveGoldCoinIcon)
        Me.Controls.Add(Me.btnUpdateParcelIcon)
        Me.Controls.Add(Me.btnAddParcelIcon)
        Me.Controls.Add(Me.btnUpdateGoldCoinIcon)
        Me.Controls.Add(Me.btnAddGoldCoinIcon)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmIcon"
        Me.Text = "Icon"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnAddGoldCoinIcon As System.Windows.Forms.Button
    Friend WithEvents btnUpdateGoldCoinIcon As System.Windows.Forms.Button
    Friend WithEvents btnAddParcelIcon As System.Windows.Forms.Button
    Friend WithEvents btnUpdateParcelIcon As System.Windows.Forms.Button
    Friend WithEvents btnRemoveGoldCoinIcon As System.Windows.Forms.Button
    Friend WithEvents btnRemoveParcelIcon As System.Windows.Forms.Button
    Friend WithEvents btnRemoveAll As System.Windows.Forms.Button
End Class
