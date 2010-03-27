Imports Tibia

Public Class frmInventory

    Private Sub btnGetItems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetItems.Click
        If client.LoggedIn Then
            lvInventory.Items.Clear()
            For Each i As Objects.Item In client.Inventory.GetItems
                lvInventory.Items.Add(New ListViewItem(New String() {i.Id.ToString, i.Count.ToString, _
                                                                     i.Width.ToString, i.Height.ToString, _
                                                                     i.Unknown1.ToString, i.Layers.ToString, i.PatternX.ToString, _
                                                                     i.PatternY.ToString, i.PatternDepth.ToString, i.Phase.ToString, _
                                                                     i.SpriteCount.ToString, i.Flags.ToString, _
                                                                     i.WalkSpeed.ToString, i.TextLimit.ToString, i.LightRadius.ToString, _
                                                                     i.LightColor.ToString, i.ShiftX.ToString, i.ShiftY.ToString, _
                                                                     i.WalkHeight.ToString, i.AutomapColor.ToString, i.LensHelp.ToString, _
                                                                     i.TopOrder.ToString, i.HasExtraByte.ToString}))
            Next
        Else
            MessageBox.Show("Please login to perform this action.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class