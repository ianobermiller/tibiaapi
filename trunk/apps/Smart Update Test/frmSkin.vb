Public Class frmSkin

    Private Sub btnAddSkin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSkin.Click
        client.Skin.AddSkin(1000, 10, 10, 32, 32, 159)
    End Sub

    Private Sub btnUpdateSkin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateSkin.Click
        client.Skin.UpdateSkin(1000, 10, 10, 32, 32, 161)
    End Sub

    Private Sub btnRemoveSkin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveSkin.Click
        client.Skin.RemoveSkin(1000)
    End Sub

    Private Sub btnRemoveAllSkins_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveAllSkins.Click
        client.Skin.RemoveAll()
    End Sub
End Class