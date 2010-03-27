<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBattlelist
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
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("")
        Me.btnGetCreatures = New System.Windows.Forms.Button
        Me.chId = New System.Windows.Forms.ColumnHeader
        Me.chName = New System.Windows.Forms.ColumnHeader
        Me.lvBattlelist = New System.Windows.Forms.ListView
        Me.chLocation = New System.Windows.Forms.ColumnHeader
        Me.chIsWalking = New System.Windows.Forms.ColumnHeader
        Me.chWalkSpeed = New System.Windows.Forms.ColumnHeader
        Me.chDirection = New System.Windows.Forms.ColumnHeader
        Me.chIsVisible = New System.Windows.Forms.ColumnHeader
        Me.chLight = New System.Windows.Forms.ColumnHeader
        Me.chLightColor = New System.Windows.Forms.ColumnHeader
        Me.chHealthBar = New System.Windows.Forms.ColumnHeader
        Me.chBlackSquare = New System.Windows.Forms.ColumnHeader
        Me.chSkull = New System.Windows.Forms.ColumnHeader
        Me.chPartyShield = New System.Windows.Forms.ColumnHeader
        Me.chWarIcon = New System.Windows.Forms.ColumnHeader
        Me.chIsBlocking = New System.Windows.Forms.ColumnHeader
        Me.chOutfit = New System.Windows.Forms.ColumnHeader
        Me.chkShowInvisible = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btnGetCreatures
        '
        Me.btnGetCreatures.Location = New System.Drawing.Point(341, 231)
        Me.btnGetCreatures.Name = "btnGetCreatures"
        Me.btnGetCreatures.Size = New System.Drawing.Size(80, 23)
        Me.btnGetCreatures.TabIndex = 2
        Me.btnGetCreatures.Text = "Get Creatures"
        Me.btnGetCreatures.UseVisualStyleBackColor = True
        '
        'chId
        '
        Me.chId.Text = "Id"
        '
        'chName
        '
        Me.chName.Text = "Name"
        '
        'lvBattlelist
        '
        Me.lvBattlelist.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chId, Me.chName, Me.chLocation, Me.chIsWalking, Me.chWalkSpeed, Me.chDirection, Me.chIsVisible, Me.chLight, Me.chLightColor, Me.chHealthBar, Me.chBlackSquare, Me.chSkull, Me.chPartyShield, Me.chWarIcon, Me.chIsBlocking, Me.chOutfit})
        Me.lvBattlelist.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvBattlelist.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1})
        Me.lvBattlelist.Location = New System.Drawing.Point(12, 12)
        Me.lvBattlelist.MultiSelect = False
        Me.lvBattlelist.Name = "lvBattlelist"
        Me.lvBattlelist.Size = New System.Drawing.Size(409, 213)
        Me.lvBattlelist.TabIndex = 3
        Me.lvBattlelist.TabStop = False
        Me.lvBattlelist.UseCompatibleStateImageBehavior = False
        Me.lvBattlelist.View = System.Windows.Forms.View.Details
        '
        'chLocation
        '
        Me.chLocation.Text = "Location"
        '
        'chIsWalking
        '
        Me.chIsWalking.Text = "IsWalking"
        '
        'chWalkSpeed
        '
        Me.chWalkSpeed.Text = "Walk Speed"
        '
        'chDirection
        '
        Me.chDirection.Text = "Direction"
        '
        'chIsVisible
        '
        Me.chIsVisible.Text = "IsVisible"
        '
        'chLight
        '
        Me.chLight.Text = "Light"
        '
        'chLightColor
        '
        Me.chLightColor.Text = "Light Color"
        '
        'chHealthBar
        '
        Me.chHealthBar.Text = "Health Bar"
        '
        'chBlackSquare
        '
        Me.chBlackSquare.Text = "Black Square"
        '
        'chSkull
        '
        Me.chSkull.Text = "Skull"
        '
        'chPartyShield
        '
        Me.chPartyShield.Text = "Party Shield"
        '
        'chWarIcon
        '
        Me.chWarIcon.Text = "War Icon"
        '
        'chIsBlocking
        '
        Me.chIsBlocking.Text = "IsBlocking"
        '
        'chOutfit
        '
        Me.chOutfit.Text = "Outfit"
        '
        'chkShowInvisible
        '
        Me.chkShowInvisible.AutoSize = True
        Me.chkShowInvisible.Location = New System.Drawing.Point(12, 235)
        Me.chkShowInvisible.Name = "chkShowInvisible"
        Me.chkShowInvisible.Size = New System.Drawing.Size(94, 17)
        Me.chkShowInvisible.TabIndex = 4
        Me.chkShowInvisible.Text = "Show Invisible"
        Me.chkShowInvisible.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Enabled = False
        Me.Label1.Location = New System.Drawing.Point(12, 255)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(272, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Note: Show invisible only works prior to the 8.41 update."
        '
        'frmBattlelist
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(433, 276)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.chkShowInvisible)
        Me.Controls.Add(Me.lvBattlelist)
        Me.Controls.Add(Me.btnGetCreatures)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmBattlelist"
        Me.Text = "Battlelist"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnGetCreatures As System.Windows.Forms.Button
    Friend WithEvents chId As System.Windows.Forms.ColumnHeader
    Friend WithEvents chName As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvBattlelist As System.Windows.Forms.ListView
    Friend WithEvents chLocation As System.Windows.Forms.ColumnHeader
    Friend WithEvents chIsWalking As System.Windows.Forms.ColumnHeader
    Friend WithEvents chWalkSpeed As System.Windows.Forms.ColumnHeader
    Friend WithEvents chDirection As System.Windows.Forms.ColumnHeader
    Friend WithEvents chIsVisible As System.Windows.Forms.ColumnHeader
    Friend WithEvents chLight As System.Windows.Forms.ColumnHeader
    Friend WithEvents chLightColor As System.Windows.Forms.ColumnHeader
    Friend WithEvents chHealthBar As System.Windows.Forms.ColumnHeader
    Friend WithEvents chBlackSquare As System.Windows.Forms.ColumnHeader
    Friend WithEvents chSkull As System.Windows.Forms.ColumnHeader
    Friend WithEvents chPartyShield As System.Windows.Forms.ColumnHeader
    Friend WithEvents chWarIcon As System.Windows.Forms.ColumnHeader
    Friend WithEvents chIsBlocking As System.Windows.Forms.ColumnHeader
    Friend WithEvents chOutfit As System.Windows.Forms.ColumnHeader
    Friend WithEvents chkShowInvisible As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
