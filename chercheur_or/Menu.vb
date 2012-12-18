Public Class Menu

    Dim boutonNouveauJoueur As New Button
    Dim panelTexte As New Panel
    'Chargement initial du menu
    Private Sub Menu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'On afffiche uniquement l'écran d'accueil
        panelAccueil.Visible = True
        panelCreationGrille.Visible = False
        boutonMenuPrincipal.Visible = False
        panelTexte.Visible = False

        'On propose 3 options de grille à l'utilisateur
        comboboxTailleGrille.Items.Insert(0, "10")
        comboboxTailleGrille.Items.Insert(1, "15")
        comboboxTailleGrille.Items.Insert(2, "20")
    End Sub
    'Si l'on modifie la taille de la grille
    Private Sub taille_grille_combobox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboboxTailleGrille.SelectedIndexChanged
        'Effacement des précèdentes selections dans les combobox (nombre de pépites et de dynamites)
        comboboxNbPepites.SelectedIndex = -1
        comboboxNbPepites.Items.Clear()
        comboboxNbDynamites.SelectedIndex = -1
        comboboxNbDynamites.Items.Clear()

        'Si l'on selectionne une grille 10*10
        If (comboboxTailleGrille.SelectedIndex = 0) Then
            comboboxNbPepites.Items.Insert(0, "10")
            comboboxNbPepites.Items.Insert(1, "15")
            comboboxNbPepites.Items.Insert(2, "20")
            comboboxNbDynamites.Items.Insert(0, "10")
            comboboxNbDynamites.Items.Insert(1, "15")
            comboboxNbDynamites.Items.Insert(2, "20")
            'Paramètres pour une grille 15*15
        ElseIf (comboboxTailleGrille.SelectedIndex = 1) Then
            comboboxNbPepites.Items.Insert(0, "20")
            comboboxNbPepites.Items.Insert(1, "25")
            comboboxNbPepites.Items.Insert(2, "30")
            comboboxNbDynamites.Items.Insert(0, "20")
            comboboxNbDynamites.Items.Insert(1, "25")
            comboboxNbDynamites.Items.Insert(2, "30")
            'Paramètres pour une grille 20*20
        ElseIf (comboboxTailleGrille.SelectedIndex = 2) Then
            comboboxNbPepites.Items.Insert(0, "30")
            comboboxNbPepites.Items.Insert(1, "35")
            comboboxNbPepites.Items.Insert(2, "40")
            comboboxNbDynamites.Items.Insert(0, "30")
            comboboxNbDynamites.Items.Insert(1, "35")
            comboboxNbDynamites.Items.Insert(2, "40")
        End If
    End Sub
    'Si l'on clique sur le bouton jouer du menu principal
    Private Sub jouer_accueil_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles boutonJouerAccueil.Click
        'On cache désormais l'écran d'accueil et on affiche l'écran des paramètres de création d'une partie, par ailleurs on affiche le bouton retour au menu principal et crédits
        panelAccueil.Visible = False
        panelCreationGrille.Visible = True
        boutonMenuPrincipal.Visible = True
        boutonCredits.Visible = True
        panelDynamique.Visible = True
        panelTexte.Visible = False

        'Par défaut le mode se joue à un joueur
        Dim textboxJoueur1 As New TextBox
        textboxJoueur1.Size = New Size(80, 20)
        textboxJoueur1.Text = "Joueur 1"
        textboxJoueur1.Location = New Point(27, 0)
        listJoueurs.Add(textboxJoueur1)

        'On donne la possibilité à l'utilisateur d'ajouter d'autres joueurs grâce à un bouton ajouter un nouveau joueur
        boutonNouveauJoueur = New Button
        boutonNouveauJoueur.Size = New Size(110, 20)
        boutonNouveauJoueur.Text = "Ajouter un joueur"
        boutonNouveauJoueur.Location = New Point(115, 0)

        'On ajoute un évenement sur notre bouton 
        AddHandler boutonNouveauJoueur.Click, AddressOf Me.boutonNouveauJoueur_Click

        'On affiche grâce à l'appel de notre fonction
        Affichage()
    End Sub

    'Création de deux listes, une comportant les boutons - et une autre comportant les joueurs
    Dim listMoins As New List(Of Button)
    Dim listJoueurs As New List(Of TextBox)

    'Lorsqu'on clique sur le bouton ajouter un nouveau joueur
    Private Sub boutonNouveauJoueur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'On s'occupe maintenant d'ajouter un nouveau textbox joueur qu'on enregistre dans notre liste de joueurs
        Dim joueurX As TextBox
        joueurX = New TextBox
        joueurX.Size = New Size(80, 20)
        joueurX.Name = "joueur" & listJoueurs.Count + 1
        joueurX.Text = "Joueur " & listJoueurs.Count + 1
        listJoueurs.Add(joueurX)

        'On s'occupe maintenant d'ajouter un nouveau bouton moins qu'on enregistre dans notre liste de boutons moins
        Dim boutonMoins As Button
        boutonMoins = New Button
        boutonMoins.Size = New Size(20, 20)

        'Le name nous permettra de retrouver nos textbox joueurs
        boutonMoins.Name = listJoueurs.Count - 1
        boutonMoins.Text = "-"
        AddHandler boutonMoins.Click, AddressOf Me.enleverJoueur
        listMoins.Add(boutonMoins)

        'On appelle notre fonction pour afficher nos listes de moins, de joueurs et notre bouton ajouter un nouveau joueur
        Affichage()
    End Sub
    'Fonction pour afficher nos listes de moins, de joueurs et notre bouton ajouter un nouveau joueur
    Private Sub Affichage()
        'On efface tout ce que contient notre panel
        panelDynamique.Controls.Clear()

        'On donne une ordonnée pour notre premier bouton moins puis on affiche chaque bouton, en décalant de 20 pour éviter une superposition des boutons
        Dim positiony As Integer = 30
        For Each moins As Button In listMoins
            moins.Location = New Point(5, positiony)
            panelDynamique.Controls.Add(moins)
            positiony = positiony + 20
        Next

        'Redéfinition de l'ordonnée et affichage de chaque bouton décalée de 20 d'ordonnée
        positiony = 10
        For Each joueur As TextBox In listJoueurs
            joueur.Location = New Point(25, positiony)
            panelDynamique.Controls.Add(joueur)
            positiony = positiony + 20
        Next

        'On vérifie qu'il y a moins de 5 joueurs sinon cela sort de notre fenêtre
        If (listJoueurs.Count < 5) Then
            'Décalage de 20 d'ordonnée le bouton ajouter un nouveau joueur
            boutonNouveauJoueur.Location = New Point(panelDynamique.Location.X + 100, positiony - 20)
            panelDynamique.Controls.Add(boutonNouveauJoueur)
        End If
    End Sub

    'Lorsqu'on enlève un joueur avec le bouton moins
    Private Sub enleverJoueur(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'On recherche notre joueur à l'aide du nom de notre bouton moins sur lequel nous venons de cliquer
        Dim joueur As Object = listJoueurs.Item(Int(sender.Name))

        'On supprime notre objet de type textbox joueur et notre objet bouton moins
        listJoueurs.Remove(joueur)
        listMoins.Remove(sender)

        'On doit maintenant modifier les noms des boutonsMoins pour qu'ils permettent toujours de retrouver les bons joueurs
        For i = Int(sender.Name) To listMoins.Count

            'On récupère l'objet bouton moins et le textbox joueur
            Dim joueurRecupere As Object = listJoueurs.Item(i), moinsRecupere As Object = listMoins.Item(i - 1)

            'On modifie le nom et le text de notre objet moins
            moinsRecupere.Name = Int(moinsRecupere.Name) - 1
            If (joueurRecupere.Text = "Joueur " & i + 2) Then
                joueurRecupere.Text = "Joueur " & i + 1
            End If
        Next

        'On efface puis réaffiche nos listes et notre bouton afficher un joueur
        Affichage()
    End Sub

    'Lorsqu'on est sur les règles du jeu
    Private Sub regles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles boutonRegles.Click
        'Affichage du panelTexte qui va nous servir pour afficher et cacher notre texte, du bouton menu principal et du bouton crédits
        panelAccueil.Visible = False
        panelCreationGrille.Visible = False
        panelDynamique.Visible = False
        boutonMenuPrincipal.Visible = True
        boutonCredits.Visible = True
        panelTexte.Controls.Clear()
        panelTexte.Visible = True

        'Creation de notre bitmap
        Dim bitmapCree As Bitmap = New Bitmap(500, 500)
        panelTexte.Size = New Size(500, 500)
        Dim g As Graphics = Graphics.FromImage(bitmapCree)

        'Police
        Dim policeTitre = New Font("Cooper Noir", 30)
        Dim policeParagraphe = New Font("DejaVu Sans", 11)

        'Alignement
        Dim Format As New StringFormat()
        Format.Alignment = StringAlignment.Center
        Format.LineAlignment = StringAlignment.Near

        'Rectangle permettant d'aligner notre texte
        Dim r As New RectangleF(40, 90, 300, 300)

        'Textes que l'on souhaite afficher
        g.DrawString("Règles", policeTitre, Brushes.Black, 120, 20)
        g.DrawString("Prenez votre pioche pour chercher toute l'or qui se trouve dans votre contrée, parfois vous découvrirez sous la terre de la dynamite ! Celle-ci vous permettra non plus de détruire une case comme avec votre pioche mais 9 ! Attention à en faire bonne usage car votre score est calculée sur le rapport suivant : nombre d'or/nombre de cases piochées", policeParagraphe, Brushes.Black, r, Format)
        Dim pictureboxTexteRegles As PictureBox = New PictureBox
        pictureboxTexteRegles.Size = New System.Drawing.Size(500, 500)
        pictureboxTexteRegles.Image = bitmapCree

        'Ajout dans le panel de la picturebox
        panelTexte.Controls.Add(pictureboxTexteRegles)
        'Ajout du panel dans le form actuel, cela affiche le texte
        Me.Controls.Add(panelTexte)
    End Sub

    'Lorqu'on est sur la page crédits
    Private Sub credits_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles boutonCredits.Click
        'Affichage du panelTexte et du bouton menu principal
        panelAccueil.Visible = False
        panelCreationGrille.Visible = False
        boutonMenuPrincipal.Visible = True
        boutonCredits.Visible = False
        panelTexte.Controls.Clear()
        panelTexte.Visible = True

        'Création de notre bitmap
        Dim bitmapCree As Bitmap = New Bitmap(500, 500)
        panelTexte.Size = New Size(500, 500)
        Dim g As Graphics = Graphics.FromImage(bitmapCree)

        'Polices et tailles
        Dim policeTitre = New Font("Cooper Noir", 30)
        Dim policeParagraphe = New Font("DejaVu Sans", 11)

        'Alignement
        Dim Format As New StringFormat()
        Format.Alignment = StringAlignment.Center
        Format.LineAlignment = StringAlignment.Near

        'Rectangle permettant d'aligner notre texte
        Dim r As New RectangleF(40, 90, 300, 300)

        'Création du texte
        g.DrawString("Crédits", policeTitre, Brushes.Black, 120, 20)
        g.DrawString("Launay Alexis - Fundone Théo", policeParagraphe, Brushes.Black, r, Format)

        'Enregistrement dans une picturebox
        Dim PictureBoxTexteCredits As PictureBox = New PictureBox
        PictureBoxTexteCredits.Size = New System.Drawing.Size(500, 500)
        PictureBoxTexteCredits.Image = bitmapCree

        'Ajout de la picturebox dans le panel
        panelTexte.Controls.Add(PictureBoxTexteCredits)

        'Affichage du panel dans le form Menu
        Me.Controls.Add(panelTexte)
    End Sub
    'Lorsqu'on retourne au menu principal'
    Private Sub retour_menu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles boutonMenuPrincipal.Click
        'On affiche uniquement l'écran d'accueil et le bouton crédit
        panelAccueil.Visible = True
        panelCreationGrille.Visible = False
        boutonMenuPrincipal.Visible = False
        boutonCredits.Visible = True
        panelTexte.Visible = False

        'On efface les anciens textbox joueurs et les boutons moins de nos listes
        listJoueurs.Clear()
        listMoins.Clear()
    End Sub

    Private Sub boutonJouerCreationGrille_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles boutonJouerCreationGrille.Click
        Dim tableauJoueurs(listJoueurs.Count()) As String
        Dim nbDynamites, nbPepites, tailleGrille
        For i = 0 To listJoueurs.Count() - 1
            tableauJoueurs(i) = listJoueurs.Item(i).Text
        Next
        nbDynamites = comboboxNbDynamites.SelectedItem
        nbPepites = comboboxNbPepites.SelectedItem
        tailleGrille = comboboxTailleGrille.SelectedItem

        Jeu.chargerDonnees(tableauJoueurs, tailleGrille, nbPepites, nbDynamites)
        Jeu.ShowDialog()
    End Sub
End Class
