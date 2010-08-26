Public Class frmPlayer

    Private Sub frmPlayer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not client.LoggedIn Then
            MessageBox.Show("You must be logged in to test this feature. Please login and reopen this window.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        Else
            If player Is Nothing Then
                player = client.GetPlayer
                UpdatePlayerInfo()
            End If
        End If
    End Sub

    Private Sub UpdatePlayerInfo()
        lblAxe.Text = player.Axe.ToString
        lblAxePercent.Text = player.AxePercent.ToString
        Dim cap As Double = Convert.ToDouble(player.Capacity) / 100
        lblCap.Text = cap.ToString
        lblClub.Text = player.Club.ToString
        lblClubPercent.Text = player.ClubPercent.ToString
        lblDistance.Text = player.Distance.ToString
        lblDistancePercent.Text = player.DistancePercent.ToString
        lblExp.Text = player.Experience.ToString
        lblFishing.Text = player.Fishing.ToString
        lblFishingPercent.Text = player.FishingPercent.ToString
        lblFist.Text = player.Fist.ToString
        lblFistPercent.Text = player.FistPercent.ToString
        lblFlags.Text = player.Flags.ToString
        lblGoToX.Text = player.GoToX.ToString
        lblGoToY.Text = player.GoToY.ToString
        lblGoToZ.Text = player.GoToZ.ToString
        lblGreenSquare.Text = player.GreenSquare.ToString
        lblHealth.Text = player.Health.ToString
        lblHealthMax.Text = player.HealthMax.ToString
        lblId.Text = player.Id.ToString
        lblLevel.Text = player.Level.ToString
        lblLevelPercent.Text = player.LevelPercent.ToString
        lblMagic.Text = player.MagicLevel.ToString
        lblMagicPercent.Text = player.MagicLevelPercent.ToString
        lblMana.Text = player.Mana.ToString
        lblManaMax.Text = player.ManaMax.ToString
        lblRedSquare.Text = player.RedSquare.ToString
        lblShielding.Text = player.Shielding.ToString
        lblShieldingPercent.Text = player.ShieldingPercent.ToString
        lblSoul.Text = player.Soul.ToString
        lblStamina.Text = player.Stamina.ToString
        lblSword.Text = player.Sword.ToString
        lblSwordPercent.Text = player.SwordPercent.ToString
        lblTargetBListId.Text = player.TargetBattlelistId.ToString
        lblTargetId.Text = player.TargetId.ToString
        lblTargetBListType.Text = player.TargetBattlelistType.ToString
        lblWhiteSquare.Text = player.WhiteSquare.ToString
        lblWorldName.Text = player.WorldName.ToString
        lblX.Text = player.X.ToString
        lblY.Text = player.Y.ToString
        lblZ.Text = player.Z.ToString
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        UpdatePlayerInfo()
    End Sub
End Class