Imports Tibia

Public Class frmBattlelist

    Private Sub btnGetCreatures_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetCreatures.Click
        If client.LoggedIn Then
            lvBattlelist.Items.Clear()
            For Each c As Objects.Creature In client.BattleList.GetCreatures
                lvBattlelist.Items.Add(New ListViewItem(New String() {c.Id.ToString, c.Name.ToString, c.Location.ToString, _
                                                                      c.IsWalking.ToString, c.WalkSpeed.ToString, c.Direction.ToString, _
                                                                      c.IsVisible.ToString, c.Light.ToString, c.LightColor.ToString, _
                                                                      c.HPBar.ToString, c.BlackSquare.ToString, c.Skull.ToString, _
                                                                      c.PartyShield.ToString, c.WarIcon.ToString, c.IsBlocking.ToString, _
                                                                      c.Outfit.LookType.ToString}))
            Next
        Else
            MessageBox.Show("Please login to perform this action.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub chkShowInvisible_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowInvisible.CheckedChanged
        If chkShowInvisible.Checked Then
            client.BattleList.ShowInvisible()
        Else
            client.BattleList.HideInvisible()
        End If
    End Sub
End Class