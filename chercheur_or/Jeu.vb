Public Class Jeu
    Const taille As Integer = 20
    Const largeurTile As Integer = 35, hauteurTile As Integer = 35

    Dim couleurSelect As Color = ColorTranslator.FromHtml("0x008fff")
    Dim couleur As Color = ColorTranslator.FromHtml("0xffffff")

    Dim terrain(taille, taille) As Integer
    Dim terrainAffichage(taille, taille) As PictureBox

    Dim tableauJoueurs(5) As String
    Dim dynamitesJoueurs() As Integer
    Dim pepitesJoueurs() As Integer
    Dim indiceJoueurEnCours As Integer

    Dim tableauStatsJoueurs As List(Of Panel)

    Const nbPepites As Integer = 10, nbDynamites As Integer = 5

    Enum CaseTerrain As Integer
        HERBE
        TERRE
        DYNAMITE
        DYNAMITE_TROUVEE
        PEPITE
        PEPITE_TROUVEE
    End Enum

    Dim dynamitesRestantesImg As PictureBox
    Dim dynamitesRestantes As Label
    Dim pepitesRestantesImg As PictureBox
    Dim pepitesRestantes As Label
    Dim herbesRestantesImg As PictureBox
    Dim herbesRestantes As Label

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
                terrainAffichage(i, j).Location = New Point(i * largeurTile, j * hauteurTile)
                terrainAffichage(i, j).Size = New Size(largeurTile, hauteurTile)
                terrainAffichage(i, j).Image = My.Resources.herbe
                terrainAffichage(i, j).Visible = True
                AddHandler terrainAffichage(i, j).MouseHover, AddressOf Me.survolSouris
                AddHandler terrainAffichage(i, j).Click, AddressOf Me.clic
                Me.Controls.Add(terrainAffichage(i, j))
            Next
        Next

        'Génération aléatoire des pépites & dynamites
        genererPepites()
        genererDynamites()

        'Génération des statistiques (création dans le code pour plus de propreté et de précision)
        genererStatsGlobales()

        'Création des joueurs et de leurs statistiques
        tableauJoueurs(0) = "Jean-Jacques"
        tableauJoueurs(1) = "Jean-Philippe"
        tableauJoueurs(2) = "Francis"
        tableauJoueurs(3) = "Jean-Kevin"
        tableauJoueurs(4) = "Jean-Christophe"

        tableauStatsJoueurs = New List(Of Panel)

        For i As Integer = 0 To Int(tableauJoueurs.Length) - 2
            creerJoueur(i)
        Next
        tableauStatsJoueurs(0).BackColor = couleurSelect
    End Sub

    Private Sub creerJoueur(ByVal numJoueur)
        tableauStatsJoueurs.Add(New Panel)
        Dim origine As Point = New Point((taille + 1) * largeurTile, hauteurTile * (numJoueur + 1) * 2 + 5 * numJoueur + 20)
        tableauStatsJoueurs(numJoueur).Location = origine
        tableauStatsJoueurs(numJoueur).Size = New Size(largeurTile * 6 + 5, hauteurTile * 2)
        tableauStatsJoueurs(numJoueur).BackColor = couleur
        tableauStatsJoueurs(numJoueur).Visible = True

        Dim nomJoueur As Label
        nomJoueur = New Label
        nomJoueur.Name = "joueur" & numJoueur
        nomJoueur.Text = tableauJoueurs(numJoueur)
        nomJoueur.Location = New Point(origine.X, origine.Y)
        nomJoueur.Size = New Size(largeurTile * 6, hauteurTile)
        nomJoueur.Visible = True
        nomJoueur.BackColor = tableauStatsJoueurs(numJoueur).BackColor
        nomJoueur.TextAlign = ContentAlignment.MiddleCenter
        tableauStatsJoueurs(numJoueur).Controls.Add(nomJoueur)
        Me.Controls.Add(nomJoueur)

        Dim dynamitesImg As PictureBox = New PictureBox
        dynamitesImg.Location = New Point(origine.X, origine.Y + hauteurTile)
        dynamitesImg.Size = New Size(largeurTile, hauteurTile)
        dynamitesImg.Image = My.Resources.dynamite
        dynamitesImg.Visible = True
        tableauStatsJoueurs(numJoueur).Controls.Add(dynamitesImg)
        Me.Controls.Add(dynamitesImg)

        Dim dynamites As Label = New Label
        dynamites.Text = "0"
        dynamites.Location = New Point(origine.X + largeurTile, origine.Y + hauteurTile)
        dynamites.Size = New Size(largeurTile, hauteurTile)
        dynamites.Visible = True
        dynamites.BackColor = tableauStatsJoueurs(numJoueur).BackColor
        dynamites.TextAlign = ContentAlignment.MiddleCenter
        tableauStatsJoueurs(numJoueur).Controls.Add(dynamites)
        Me.Controls.Add(dynamites)

        Dim herbesImg As PictureBox = New PictureBox
        herbesImg.Location = New Point(origine.X + largeurTile * 2, origine.Y + hauteurTile)
        herbesImg.Size = New Size(largeurTile, hauteurTile)
        herbesImg.Image = My.Resources.herbe
        herbesImg.Visible = True
        tableauStatsJoueurs(numJoueur).Controls.Add(herbesImg)
        Me.Controls.Add(herbesImg)

        Dim herbes As Label = New Label
        herbes.Text = "0"
        herbes.Location = New Point(origine.X + largeurTile * 3, origine.Y + hauteurTile)
        herbes.Size = New Size(largeurTile, hauteurTile)
        herbes.Visible = True
        herbes.BackColor = tableauStatsJoueurs(numJoueur).BackColor
        herbes.TextAlign = ContentAlignment.MiddleCenter
        tableauStatsJoueurs(numJoueur).Controls.Add(herbes)
        Me.Controls.Add(herbes)

        Dim pepitesImg As PictureBox = New PictureBox
        pepitesImg.Location = New Point(origine.X + largeurTile * 4, origine.Y + hauteurTile)
        pepitesImg.Size = New Size(largeurTile, hauteurTile)
        pepitesImg.Image = My.Resources._or
        pepitesImg.Visible = True
        tableauStatsJoueurs(numJoueur).Controls.Add(pepitesImg)
        Me.Controls.Add(pepitesImg)

        Dim pepites As Label
        pepites = New Label
        pepites.Text = "0"
        pepites.Location = New Point(origine.X + largeurTile * 5, origine.Y + hauteurTile)
        pepites.Size = New Size(largeurTile, hauteurTile)
        pepites.BackColor = tableauStatsJoueurs(numJoueur).BackColor
        pepites.Visible = True
        pepites.TextAlign = ContentAlignment.MiddleCenter
        tableauStatsJoueurs(numJoueur).Controls.Add(pepites)
        Me.Controls.Add(pepites)

        tableauStatsJoueurs(numJoueur).Visible = True
        Me.Controls.Add(tableauStatsJoueurs(numJoueur))
    End Sub

    Private Sub genererStatsGlobales()
        dynamitesRestantesImg = New PictureBox
        dynamitesRestantesImg.Location = New Point((taille + 1) * largeurTile, hauteurTile)
        dynamitesRestantesImg.Size = New Size(largeurTile, hauteurTile)
        dynamitesRestantesImg.Image = My.Resources.dynamite
        dynamitesRestantesImg.Visible = True
        Me.Controls.Add(dynamitesRestantesImg)

        dynamitesRestantes = New Label
        dynamitesRestantes.Text = nbDynamites.ToString
        dynamitesRestantes.Location = New Point((taille + 2) * largeurTile, hauteurTile)
        dynamitesRestantes.Size = New Size(largeurTile, hauteurTile)
        dynamitesRestantes.Visible = True
        dynamitesRestantes.TextAlign = ContentAlignment.MiddleCenter
        Me.Controls.Add(dynamitesRestantes)

        herbesRestantesImg = New PictureBox
        herbesRestantesImg.Location = New Point((taille + 3) * largeurTile, hauteurTile)
        herbesRestantesImg.Size = New Size(largeurTile, hauteurTile)
        herbesRestantesImg.Image = My.Resources.herbe
        herbesRestantesImg.Visible = True
        Me.Controls.Add(herbesRestantesImg)

        herbesRestantes = New Label
        herbesRestantes.Text = (taille * taille).ToString
        herbesRestantes.Location = New Point((taille + 4) * largeurTile, hauteurTile)
        herbesRestantes.Size = New Size(largeurTile, hauteurTile)
        herbesRestantes.Visible = True
        herbesRestantes.TextAlign = ContentAlignment.MiddleCenter
        Me.Controls.Add(herbesRestantes)

        pepitesRestantesImg = New PictureBox
        pepitesRestantesImg.Location = New Point((taille + 5) * largeurTile, hauteurTile)
        pepitesRestantesImg.Size = New Size(largeurTile, hauteurTile)
        pepitesRestantesImg.Image = My.Resources._or
        pepitesRestantesImg.Visible = True
        Me.Controls.Add(pepitesRestantesImg)

        pepitesRestantes = New Label
        pepitesRestantes.Text = nbPepites.ToString
        pepitesRestantes.Location = New Point((taille + 6) * largeurTile, hauteurTile)
        pepitesRestantes.Size = New Size(largeurTile, hauteurTile)
        pepitesRestantes.Visible = True
        pepitesRestantes.TextAlign = ContentAlignment.MiddleCenter
        Me.Controls.Add(pepitesRestantes)
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

            If (terrain(xTMP, yTMP) = CaseTerrain.HERBE And terrain(xTMP + 1, yTMP) = CaseTerrain.HERBE) Then 'On pose les deux premières pépites
                terrain(xTMP, yTMP) = CaseTerrain.PEPITE
                terrain(xTMP + 1, yTMP) = CaseTerrain.PEPITE
                compteur -= 2
                If (blocPepite >= 3 Or compteur = 1) Then 'On en pose une troisième si le bloc est de 3
                    If (terrain(xTMP, yTMP + 1) = CaseTerrain.HERBE) Then
                        terrain(xTMP, yTMP + 1) = CaseTerrain.PEPITE
                        compteur -= 1
                        If (blocPepite >= 4 Or compteur = 1) Then 'Idem avec une quatrième
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

    Private Sub clic(ByVal sender As System.Object, ByVal e As System.EventArgs)
        dynamiter(sender, e)

        If (indiceJoueurEnCours >= tableauJoueurs.Length - 2) Then 'Si c'était le dernier joueur alors on revient au début
            tableauStatsJoueurs(indiceJoueurEnCours).BackColor = couleur
            indiceJoueurEnCours = 0
            tableauStatsJoueurs(indiceJoueurEnCours).BackColor = couleurSelect
        Else
            tableauStatsJoueurs(indiceJoueurEnCours).BackColor = couleur
            indiceJoueurEnCours += 1
            tableauStatsJoueurs(indiceJoueurEnCours).BackColor = couleurSelect
        End If
    End Sub

    Private Sub piocher(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim caseCliquee As PictureBox = sender
        Dim x As Integer = Int(caseCliquee.Location.X / largeurTile), y As Integer = Int(caseCliquee.Location.Y / hauteurTile)

        Select Case terrain(x, y)
            Case CaseTerrain.PEPITE
                terrain(x, y) = CaseTerrain.PEPITE_TROUVEE
                terrainAffichage(x, y).Image = My.Resources._or
                herbesRestantes.Text = Int(herbesRestantes.Text) - 1
                pepitesRestantes.Text = Int(pepitesRestantes.Text) - 1
            Case CaseTerrain.DYNAMITE
                terrain(x, y) = CaseTerrain.DYNAMITE_TROUVEE
                terrainAffichage(x, y).Image = My.Resources.dynamite
                herbesRestantes.Text = Int(herbesRestantes.Text) - 1
                dynamitesRestantes.Text = Int(dynamitesRestantes.Text) - 1
            Case CaseTerrain.HERBE
                terrain(x, y) = CaseTerrain.TERRE
                terrainAffichage(x, y).Image = My.Resources.terre
                herbesRestantes.Text = Int(herbesRestantes.Text) - 1
        End Select
    End Sub

    Private Sub dynamiter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim caseCliquee As PictureBox = sender
        Dim x As Integer = Int(caseCliquee.Location.X / largeurTile), y As Integer = Int(caseCliquee.Location.Y / hauteurTile)
        Dim origineX As Integer = x - 1, origineY As Integer = y - 1
        Dim finX As Integer = x + 1, finY As Integer = y + 1

        'On calcule notre carré impact
        If (origineX < 0) Then
            origineX = 0
        End If
        If (origineY < 0) Then
            origineY = 0
        End If
        If (finY > taille - 1) Then
            finY = taille - 1
        End If
        If (finX > taille - 1) Then
            finX = taille - 1
        End If

        For i As Integer = origineX To finX
            For j As Integer = origineY To finY
                piocher(terrainAffichage(i, j), System.EventArgs.Empty)
            Next
        Next
    End Sub

End Class
