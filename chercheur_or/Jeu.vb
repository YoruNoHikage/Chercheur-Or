Public Class Jeu
    Const taille As Integer = 20
    Const largeurTile As Integer = 35, hauteurTile As Integer = 35

    Dim terrain(taille, taille) As Integer
    Dim terrainAffichage(taille, taille) As PictureBox

    Const nbPepites As Integer = 10, nbDynamites As Integer = 5

    Enum CaseTerrain As Integer
        HERBE
        TERRE
        DYNAMITE
        DYNAMITE_TROUVEE
        PEPITE
        PEPITE_TROUVEE
    End Enum

    Private Sub chargementFenetre(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Randomize()

        'Configuration de la fenêtre de base
        Me.Text = "Le Chercheur d'Or"
        Me.Width = 1024
        Me.Height = 768

        'Création du terrain
        For i As Integer = 0 To taille - 1
            For j As Integer = 0 To taille - 1
                terrain(i, j) = CaseTerrain.HERBE 'On remplit le terrain d'herbe

                terrainAffichage(i, j) = New PictureBox
                terrainAffichage(i, j).Location = New Point(i * largeurTile, j * largeurTile)
                terrainAffichage(i, j).Size = New Size(largeurTile, hauteurTile)
                terrainAffichage(i, j).Image = My.Resources.herbe
                terrainAffichage(i, j).Visible = True
                AddHandler terrainAffichage(i, j).MouseHover, AddressOf Me.survolSouris
                AddHandler terrainAffichage(i, j).Click, AddressOf Me.piocher
                Me.Controls.Add(terrainAffichage(i, j))
            Next
        Next

        'Génération aléatoire des pépites & dynamites
        genererPepites()
        genererDynamites()
    End Sub

    Private Sub genererPepites()
        Dim compteur As Integer = nbPepites
        While compteur > 0 'Tant qu'on a pas posé toutes les pépites on continue
            Dim blocPepite As Integer
            If (compteur > 4) Then
                blocPepite = Int(4 * Rnd() + 2)
            ElseIf (compteur = 4 Or compteur = 3 Or compteur = 2) Then
                blocPepite = compteur
            End If

            Dim xTMP As Integer = Int((taille - 1) * Rnd()), yTMP As Integer = Int((taille - 1) * Rnd())

            If (terrain(xTMP, yTMP) = CaseTerrain.HERBE And terrain(xTMP + 1, yTMP) = CaseTerrain.HERBE) Then
                terrain(xTMP, yTMP) = CaseTerrain.PEPITE
                terrain(xTMP + 1, yTMP) = CaseTerrain.PEPITE
                compteur -= 2
                If (blocPepite >= 3) Then
                    If (terrain(xTMP, yTMP + 1) = CaseTerrain.HERBE) Then
                        terrain(xTMP, yTMP + 1) = CaseTerrain.PEPITE
                        compteur -= 1
                        If (blocPepite >= 4) Then
                            If (terrain(xTMP + 1, yTMP + 1) = CaseTerrain.HERBE) Then
                                terrain(xTMP + 1, yTMP + 1) = CaseTerrain.PEPITE
                                compteur -= 1
                            End If
                        End If
                    End If
                End If
            End If

        End While
    End Sub

    Private Sub genererDynamites()
        Dim compteur As Integer = nbDynamites
        While compteur > 0
            Dim xTMP As Integer = Int((taille - 1) * Rnd()), yTMP As Integer = Int((taille - 1) * Rnd())
            If (terrain(xTMP, yTMP) = CaseTerrain.HERBE) Then
                terrain(xTMP, yTMP) = CaseTerrain.DYNAMITE
                compteur -= 1
            End If
        End While
    End Sub

    Dim coordonneesPrec As Point
    Private Sub survolSouris(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim caseSurvole As PictureBox = sender

        If (coordonneesPrec.X >= 0 And coordonneesPrec.X < taille And coordonneesPrec.Y >= 0 And coordonneesPrec.Y < taille And terrain(coordonneesPrec.X, coordonneesPrec.Y) = CaseTerrain.HERBE) Then
            terrainAffichage(coordonneesPrec.X, coordonneesPrec.Y).Image = My.Resources.herbe
        End If

        If (terrain(Int(caseSurvole.Location.X / largeurTile), Int(caseSurvole.Location.Y / hauteurTile)) = CaseTerrain.HERBE) Then
            caseSurvole.Image = My.Resources.survol
            coordonneesPrec = New Point(Int(caseSurvole.Location.X / largeurTile), Int(caseSurvole.Location.Y / hauteurTile))
        End If
    End Sub

    Private Sub piocher(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim caseCliquee As PictureBox = sender
        Dim x As Integer = Int(caseCliquee.Location.X / largeurTile), y As Integer = Int(caseCliquee.Location.Y / hauteurTile)

        Select Case terrain(x, y)
            Case CaseTerrain.PEPITE
                terrain(x, y) = CaseTerrain.PEPITE_TROUVEE
                terrainAffichage(x, y).Image = My.Resources._or
            Case CaseTerrain.DYNAMITE
                terrain(x, y) = CaseTerrain.DYNAMITE_TROUVEE
                terrainAffichage(x, y).Image = My.Resources.dynamite
            Case CaseTerrain.HERBE
                terrain(x, y) = CaseTerrain.TERRE
                terrainAffichage(x, y).Image = My.Resources.terre
        End Select
    End Sub

End Class
