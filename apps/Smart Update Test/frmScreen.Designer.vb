<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScreen
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
        Me.btnAddScreenText = New System.Windows.Forms.Button
        Me.btnAddCreatureTextId = New System.Windows.Forms.Button
        Me.btnAddCreatureTextName = New System.Windows.Forms.Button
        Me.btnRemoveScreenText = New System.Windows.Forms.Button
        Me.btnUpdateCreatureTextId = New System.Windows.Forms.Button
        Me.btnUpdateCreatureTextName = New System.Windows.Forms.Button
        Me.btnRemoveCreatureTextId = New System.Windows.Forms.Button
        Me.btnRemoveCreatureTextName = New System.Windows.Forms.Button
        Me.btnRemoveAllText = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnAddScreenText
        '
        Me.btnAddScreenText.Location = New System.Drawing.Point(12, 12)
        Me.btnAddScreenText.Name = "btnAddScreenText"
        Me.btnAddScreenText.Size = New System.Drawing.Size(157, 23)
        Me.btnAddScreenText.TabIndex = 0
        Me.btnAddScreenText.Text = "Add Screen Text"
        Me.btnAddScreenText.UseVisualStyleBackColor = True
        '
        'btnAddCreatureTextId
        '
        Me.btnAddCreatureTextId.Location = New System.Drawing.Point(12, 41)
        Me.btnAddCreatureTextId.Name = "btnAddCreatureTextId"
        Me.btnAddCreatureTextId.Size = New System.Drawing.Size(157, 23)
        Me.btnAddCreatureTextId.TabIndex = 1
        Me.btnAddCreatureTextId.Text = "Add Creature Text: Id"
        Me.btnAddCreatureTextId.UseVisualStyleBackColor = True
        '
        'btnAddCreatureTextName
        '
        Me.btnAddCreatureTextName.Location = New System.Drawing.Point(12, 70)
        Me.btnAddCreatureTextName.Name = "btnAddCreatureTextName"
        Me.btnAddCreatureTextName.Size = New System.Drawing.Size(157, 23)
        Me.btnAddCreatureTextName.TabIndex = 2
        Me.btnAddCreatureTextName.Text = "Add Creature Text: Name"
        Me.btnAddCreatureTextName.UseVisualStyleBackColor = True
        '
        'btnRemoveScreenText
        '
        Me.btnRemoveScreenText.Location = New System.Drawing.Point(175, 12)
        Me.btnRemoveScreenText.Name = "btnRemoveScreenText"
        Me.btnRemoveScreenText.Size = New System.Drawing.Size(157, 23)
        Me.btnRemoveScreenText.TabIndex = 3
        Me.btnRemoveScreenText.Text = "Remove Screen Text"
        Me.btnRemoveScreenText.UseVisualStyleBackColor = True
        '
        'btnUpdateCreatureTextId
        '
        Me.btnUpdateCreatureTextId.Location = New System.Drawing.Point(175, 41)
        Me.btnUpdateCreatureTextId.Name = "btnUpdateCreatureTextId"
        Me.btnUpdateCreatureTextId.Size = New System.Drawing.Size(157, 23)
        Me.btnUpdateCreatureTextId.TabIndex = 4
        Me.btnUpdateCreatureTextId.Text = "Update Creature Text: Id"
        Me.btnUpdateCreatureTextId.UseVisualStyleBackColor = True
        '
        'btnUpdateCreatureTextName
        '
        Me.btnUpdateCreatureTextName.Location = New System.Drawing.Point(175, 70)
        Me.btnUpdateCreatureTextName.Name = "btnUpdateCreatureTextName"
        Me.btnUpdateCreatureTextName.Size = New System.Drawing.Size(157, 23)
        Me.btnUpdateCreatureTextName.TabIndex = 5
        Me.btnUpdateCreatureTextName.Text = "Update Creature Text: Name"
        Me.btnUpdateCreatureTextName.UseVisualStyleBackColor = True
        '
        'btnRemoveCreatureTextId
        '
        Me.btnRemoveCreatureTextId.Location = New System.Drawing.Point(338, 41)
        Me.btnRemoveCreatureTextId.Name = "btnRemoveCreatureTextId"
        Me.btnRemoveCreatureTextId.Size = New System.Drawing.Size(157, 23)
        Me.btnRemoveCreatureTextId.TabIndex = 6
        Me.btnRemoveCreatureTextId.Text = "Remove Creature Text: Id"
        Me.btnRemoveCreatureTextId.UseVisualStyleBackColor = True
        '
        'btnRemoveCreatureTextName
        '
        Me.btnRemoveCreatureTextName.Location = New System.Drawing.Point(338, 70)
        Me.btnRemoveCreatureTextName.Name = "btnRemoveCreatureTextName"
        Me.btnRemoveCreatureTextName.Size = New System.Drawing.Size(157, 23)
        Me.btnRemoveCreatureTextName.TabIndex = 7
        Me.btnRemoveCreatureTextName.Text = "Remove Creature Text: Name"
        Me.btnRemoveCreatureTextName.UseVisualStyleBackColor = True
        '
        'btnRemoveAllText
        '
        Me.btnRemoveAllText.Location = New System.Drawing.Point(338, 12)
        Me.btnRemoveAllText.Name = "btnRemoveAllText"
        Me.btnRemoveAllText.Size = New System.Drawing.Size(157, 23)
        Me.btnRemoveAllText.TabIndex = 8
        Me.btnRemoveAllText.Text = "Remove All Screen Text"
        Me.btnRemoveAllText.UseVisualStyleBackColor = True
        '
        'frmScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(506, 104)
        Me.Controls.Add(Me.btnRemoveAllText)
        Me.Controls.Add(Me.btnRemoveCreatureTextName)
        Me.Controls.Add(Me.btnRemoveCreatureTextId)
        Me.Controls.Add(Me.btnUpdateCreatureTextName)
        Me.Controls.Add(Me.btnUpdateCreatureTextId)
        Me.Controls.Add(Me.btnRemoveScreenText)
        Me.Controls.Add(Me.btnAddCreatureTextName)
        Me.Controls.Add(Me.btnAddCreatureTextId)
        Me.Controls.Add(Me.btnAddScreenText)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmScreen"
        Me.Text = "Screen"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnAddScreenText As System.Windows.Forms.Button
    Friend WithEvents btnAddCreatureTextId As System.Windows.Forms.Button
    Friend WithEvents btnAddCreatureTextName As System.Windows.Forms.Button
    Friend WithEvents btnRemoveScreenText As System.Windows.Forms.Button
    Friend WithEvents btnUpdateCreatureTextId As System.Windows.Forms.Button
    Friend WithEvents btnUpdateCreatureTextName As System.Windows.Forms.Button
    Friend WithEvents btnRemoveCreatureTextId As System.Windows.Forms.Button
    Friend WithEvents btnRemoveCreatureTextName As System.Windows.Forms.Button
    Friend WithEvents btnRemoveAllText As System.Windows.Forms.Button
End Class
