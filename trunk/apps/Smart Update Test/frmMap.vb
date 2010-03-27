Imports Tibia

Public Class frmMap
    Private CurrentFloor As Integer = 0

    Private Sub btnReplaceTrees_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReplaceTrees.Click
        client.Map.ReplaceTrees()
    End Sub

    Private Sub chkFullLightOn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFullLightOn.CheckedChanged
        If chkFullLightOn.Checked Then
            client.Map.FullLightOn()
        Else
            client.Map.FullLightOff()
        End If
    End Sub

    Private Sub chkNameSpyOn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNameSpyOn.CheckedChanged
        If chkNameSpyOn.Checked Then
            client.Map.NameSpyOn()
        Else
            client.Map.NameSpyOff()
        End If
    End Sub

    Private Sub chkLevelSpyOn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLevelSpyOn.CheckedChanged
        If player IsNot Nothing Then
            If chkLevelSpyOn.Checked Then
                CurrentFloor = player.Location.Z - 7
                client.Map.LevelSpyOn(CurrentFloor)
            Else
                client.Map.LevelSpyOff()
            End If
        Else
            MessageBox.Show("Player object is nothing.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error)
            If client.LoggedIn Then
                player = client.GetPlayer
            End If
        End If
    End Sub

    Private Sub btnFloorUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFloorUp.Click
        If chkLevelSpyOn.Checked Then
            CurrentFloor += 1
            client.Map.LevelSpyOn(CurrentFloor)
        End If
    End Sub

    Private Sub btnFloorDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFloorDown.Click
        If chkLevelSpyOn.Checked Then
            If CurrentFloor > 0 Then
                CurrentFloor -= 1
                client.Map.LevelSpyOn(CurrentFloor)
            End If
        End If
    End Sub

    Private Sub btnCheckMapTiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckMapTiles.Click
        If player IsNot Nothing Then
            Dim TilesOnCurrentFloor As IEnumerable(Of Objects.Tile) = client.Map.GetTilesOnSameFloor
            If TilesOnCurrentFloor(105).Location = player.Location Then
                MessageBox.Show("Map tiles are correct.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                MessageBox.Show("Map tiles are incorrect.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Else
            MessageBox.Show("Player object is nothing.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error)
            If client.LoggedIn Then
                player = client.GetPlayer
            End If
        End If
    End Sub

    Private Sub frmMap_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not client.LoggedIn Then
            MessageBox.Show("You must be logged in to test these features. Please login and reopen this window.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
    End Sub
End Class