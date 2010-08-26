<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInventory
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
        Me.lvInventory = New System.Windows.Forms.ListView()
        Me.chId = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chCount = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chWidth = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chHeight = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chUnknown1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chLayers = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chPatternX = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chPatternY = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chPatternDepth = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chPhase = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chSpriteCount = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chFlags = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chWalkSpeed = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chTextLimit = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chLightRadius = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chLightColor = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chShiftX = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chShiftY = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chWalkHeight = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chAutomapColor = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chLensHelp = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chTopOrder = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chHasExtraByte = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnGetItems = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lvInventory
        '
        Me.lvInventory.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chId, Me.chCount, Me.chWidth, Me.chHeight, Me.chUnknown1, Me.chLayers, Me.chPatternX, Me.chPatternY, Me.chPatternDepth, Me.chPhase, Me.chSpriteCount, Me.chFlags, Me.chWalkSpeed, Me.chTextLimit, Me.chLightRadius, Me.chLightColor, Me.chShiftX, Me.chShiftY, Me.chWalkHeight, Me.chAutomapColor, Me.chLensHelp, Me.chTopOrder, Me.chHasExtraByte})
        Me.lvInventory.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvInventory.Location = New System.Drawing.Point(12, 12)
        Me.lvInventory.MultiSelect = False
        Me.lvInventory.Name = "lvInventory"
        Me.lvInventory.Size = New System.Drawing.Size(408, 248)
        Me.lvInventory.TabIndex = 0
        Me.lvInventory.UseCompatibleStateImageBehavior = False
        Me.lvInventory.View = System.Windows.Forms.View.Details
        '
        'chId
        '
        Me.chId.Text = "Id"
        '
        'chCount
        '
        Me.chCount.Text = "Count"
        '
        'chWidth
        '
        Me.chWidth.Text = "Width"
        '
        'chHeight
        '
        Me.chHeight.Text = "Height"
        '
        'chUnknown1
        '
        Me.chUnknown1.Text = "Unknown1"
        '
        'chLayers
        '
        Me.chLayers.Text = "Layers"
        '
        'chPatternX
        '
        Me.chPatternX.Text = "Pattern X"
        '
        'chPatternY
        '
        Me.chPatternY.Text = "Pattern Y"
        '
        'chPatternDepth
        '
        Me.chPatternDepth.Text = "Pattern Depth"
        '
        'chPhase
        '
        Me.chPhase.Text = "Phase"
        '
        'chSpriteCount
        '
        Me.chSpriteCount.Text = "Sprite Count"
        '
        'chFlags
        '
        Me.chFlags.Text = "Flags"
        '
        'chWalkSpeed
        '
        Me.chWalkSpeed.Text = "Walk Speed"
        '
        'chTextLimit
        '
        Me.chTextLimit.Text = "Text Limit"
        '
        'chLightRadius
        '
        Me.chLightRadius.Text = "Light Radius"
        '
        'chLightColor
        '
        Me.chLightColor.Text = "Light Color"
        '
        'chShiftX
        '
        Me.chShiftX.Text = "Shift X"
        '
        'chShiftY
        '
        Me.chShiftY.Text = "Shift Y"
        '
        'chWalkHeight
        '
        Me.chWalkHeight.Text = "Walk Height"
        '
        'chAutomapColor
        '
        Me.chAutomapColor.Text = "Automap Color"
        '
        'chLensHelp
        '
        Me.chLensHelp.Text = "LensHelp"
        '
        'chTopOrder
        '
        Me.chTopOrder.Text = "Top Order"
        '
        'chHasExtraByte
        '
        Me.chHasExtraByte.Text = "Has Extra Byte"
        '
        'btnGetItems
        '
        Me.btnGetItems.Location = New System.Drawing.Point(345, 266)
        Me.btnGetItems.Name = "btnGetItems"
        Me.btnGetItems.Size = New System.Drawing.Size(75, 23)
        Me.btnGetItems.TabIndex = 1
        Me.btnGetItems.Text = "Get Items"
        Me.btnGetItems.UseVisualStyleBackColor = True
        '
        'frmInventory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(432, 295)
        Me.Controls.Add(Me.btnGetItems)
        Me.Controls.Add(Me.lvInventory)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmInventory"
        Me.Text = "Inventory"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lvInventory As System.Windows.Forms.ListView
    Friend WithEvents chId As System.Windows.Forms.ColumnHeader
    Friend WithEvents chCount As System.Windows.Forms.ColumnHeader
    Friend WithEvents chWidth As System.Windows.Forms.ColumnHeader
    Friend WithEvents chHeight As System.Windows.Forms.ColumnHeader
    Friend WithEvents chUnknown1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents chLayers As System.Windows.Forms.ColumnHeader
    Friend WithEvents chPatternX As System.Windows.Forms.ColumnHeader
    Friend WithEvents chPatternY As System.Windows.Forms.ColumnHeader
    Friend WithEvents chPatternDepth As System.Windows.Forms.ColumnHeader
    Friend WithEvents chPhase As System.Windows.Forms.ColumnHeader
    Friend WithEvents chSpriteCount As System.Windows.Forms.ColumnHeader
    Friend WithEvents chFlags As System.Windows.Forms.ColumnHeader
    Friend WithEvents chWalkSpeed As System.Windows.Forms.ColumnHeader
    Friend WithEvents chTextLimit As System.Windows.Forms.ColumnHeader
    Friend WithEvents chLightRadius As System.Windows.Forms.ColumnHeader
    Friend WithEvents chLightColor As System.Windows.Forms.ColumnHeader
    Friend WithEvents chShiftX As System.Windows.Forms.ColumnHeader
    Friend WithEvents chShiftY As System.Windows.Forms.ColumnHeader
    Friend WithEvents chWalkHeight As System.Windows.Forms.ColumnHeader
    Friend WithEvents chAutomapColor As System.Windows.Forms.ColumnHeader
    Friend WithEvents chLensHelp As System.Windows.Forms.ColumnHeader
    Friend WithEvents chTopOrder As System.Windows.Forms.ColumnHeader
    Friend WithEvents chHasExtraByte As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnGetItems As System.Windows.Forms.Button
End Class
