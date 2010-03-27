Imports Tibia

Public Class frmScreen

    Private Sub btnAddScreenText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddScreenText.Click
        client.Screen.DrawScreenText("Test", New Objects.Location(10, 10, 0), Color.White, Constants.ClientFont.Normal, "TibiaAPI Draw Screen Text")
    End Sub

    Private Sub btnRemoveScreenText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveScreenText.Click
        client.Screen.RemoveScreenText("Test")
    End Sub

    Private Sub btnAddCreatureTextId_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCreatureTextId.Click
        If player IsNot Nothing Then
            client.Screen.DrawCreatureText(player.Id, New Objects.Location(0, 10, 0), Color.White, Constants.ClientFont.Normal, "TibiaAPI Draw Creature Text: Id")
        Else
            MessageBox.Show("The player object is nothing. You cannot test this feature without a player object." & _
                            "If you would like to test this feature please close this window, make sure you're " & _
                            "logged in to the client, then reopen this window.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnUpdateCreatureTextId_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateCreatureTextId.Click
        If player IsNot Nothing Then
            client.Screen.UpdateCreatureText(player.Id, New Objects.Location(0, 10, 0), "TibiaAPI Update Creature Text: Id")
        Else
            MessageBox.Show("The player object is nothing. You cannot test this feature without a player object." & _
                    "If you would like to test this feature please close this window, make sure you're " & _
                    "logged in to the client, then reopen this window.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnRemoveCreatureTextId_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveCreatureTextId.Click
        If player IsNot Nothing Then
            client.Screen.RemoveCreatureText(player.Id)
        Else
            MessageBox.Show("The player object is nothing. You cannot test this feature without a player object." & _
                    "If you would like to test this feature please close this window, make sure you're " & _
                    "logged in to the client, then reopen this window.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnAddCreatureTextName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCreatureTextName.Click
        If player IsNot Nothing Then
            client.Screen.DrawCreatureText(player.Name, New Objects.Location(0, -10, 0), Color.White, Constants.ClientFont.Normal, "TibiaAPI Draw Creature Text: Name")
        Else
            MessageBox.Show("The player object is nothing. You cannot test this feature without a player object." & _
                    "If you would like to test this feature please close this window, make sure you're " & _
                    "logged in to the client, then reopen this window.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnUpdateCreatureTextName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateCreatureTextName.Click
        If player IsNot Nothing Then
            client.Screen.UpdateCreatureText(player.Name, New Objects.Location(0, -10, 0), "TibiaAPI Update Creature Text: Name")
        Else
            MessageBox.Show("The player object is nothing. You cannot test this feature without a player object." & _
                    "If you would like to test this feature please close this window, make sure you're " & _
                    "logged in to the client, then reopen this window.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnRemoveCreatureTextName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveCreatureTextName.Click
        If player IsNot Nothing Then
            client.Screen.RemoveCreatureText(player.Name)
        Else
            MessageBox.Show("The player object is nothing. You cannot test this feature without a player object." & _
                    "If you would like to test this feature please close this window, make sure you're " & _
                    "logged in to the client, then reopen this window.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnRemoveAllText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveAllText.Click
        client.Screen.RemoveAllScreenText()
    End Sub

    Private Sub frmScreen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not client.LoggedIn Then
            MessageBox.Show("You must be logged in to test the Creature Text functions. If you would like to test" & _
                            " these functions please login first.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            If player IsNot Nothing Then
                player = client.GetPlayer
            End If
        End If
    End Sub
End Class