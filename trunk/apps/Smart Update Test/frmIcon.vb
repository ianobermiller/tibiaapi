Public Class frmIcon

    Private Function OnIconClick(ByVal iconId As Integer) As Boolean
        Select Case iconId
            Case 1000
                MessageBox.Show("Gold Coin Icon clicked.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Case 1001
                MessageBox.Show("Parcel Icon clicked.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Select
    End Function

    Private Sub btnAddGoldCoinIcon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddGoldCoinIcon.Click
        client.Icon.AddIcon(1000, 10, 10, 32, 3031, 1, Tibia.Constants.ClientFont.Normal, Color.White)
    End Sub

    Private Sub btnUpdateGoldCoinIcon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateGoldCoinIcon.Click
        client.Icon.UpdateIcon(1000, 10, 10, 32, 3031, 100, Tibia.Constants.ClientFont.Normal, Color.White)
    End Sub

    Private Sub btnRemoveGoldCoinIcon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveGoldCoinIcon.Click
        client.Icon.RemoveIcon(1000)
    End Sub

    Private Sub btnAddParcelIcon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddParcelIcon.Click
        client.Icon.AddIcon(1001, 10, 52, 32, 3503, 1, Tibia.Constants.ClientFont.Normal, Color.White)
    End Sub

    Private Sub btnUpdateParcelIcon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateParcelIcon.Click
        client.Icon.UpdateIcon(1001, 10, 52, 32, 3504, 1, Tibia.Constants.ClientFont.Normal, Color.White)
    End Sub

    Private Sub btnRemoveParcelIcon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveParcelIcon.Click
        client.Icon.RemoveIcon(1001)
    End Sub

    Private Sub btnRemoveAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveAll.Click
        client.Icon.RemoveAll()
    End Sub

    Private Sub frmIcon_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddHandler client.Icon.Click, AddressOf OnIconClick
    End Sub
End Class