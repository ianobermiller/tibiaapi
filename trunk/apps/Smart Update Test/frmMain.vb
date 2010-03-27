Imports Tibia

Public Class frmMain

    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        client = Util.ClientChooser.ShowBox

        If client Is Nothing Then
            MessageBox.Show("No client found. Please restart the program.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Application.Exit()
        Else
            If client.LoggedIn Then
                player = client.GetPlayer
            End If
        End If
    End Sub

    Private Sub btnBattlelist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBattlelist.Click
        frmBattlelist.Show()
    End Sub

    Private Sub btnClient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClient.Click
        frmClient.Show()
    End Sub

    Private Sub btnContextMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContextMenu.Click
        frmContextMenu.Show()
    End Sub

    Private Sub btnIcon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIcon.Click
        frmIcon.Show()
    End Sub

    Private Sub btnInventory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInventory.Click
        frmInventory.Show()
    End Sub

    Private Sub btnMap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMap.Click
        frmMap.Show()
    End Sub

    Private Sub btnPlayer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlayer.Click
        frmPlayer.Show()
    End Sub

    Private Sub btnScreen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScreen.Click
        frmScreen.Show()
    End Sub

    Private Sub btnSkin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSkin.Click
        frmSkin.Show()
    End Sub

    Private Sub btnVIP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVIP.Click
        frmVIP.Show()
    End Sub

    Private Sub btnPackets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPackets.Click
        frmPackets.Show()
    End Sub

    Private Sub btnHotkey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHotkey.Click
        frmHotkey.Show()
    End Sub
End Class
