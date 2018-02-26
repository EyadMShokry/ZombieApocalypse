  //***********************************************//
 //* Thank you for buying Treasure Chest Maker!! *//
//***********************************************//
//
// Treasure Chest Maker (TCM) is designed to allow you to build your own unique chest with spikes, handles, wood, metal, and any color you want.
//
  ///////////////////
 //* TEST IT OUT *//
///////////////////
//
// Play around with some of the pre-made chests in the Demo scene. Load it up and hit Play. The camera pans with the mouse (like StarCraft) and clicking a chest will open it!
// Scene is located under the folder: /TreasureChestMaker/Example/Scene/Demo
//
  //////////////////////
 //* THINKS TO KNOW *//
//////////////////////
//
// The TCM folder must be located under Assets/TreasureChestMaker. Once you import the asset, it's important you do not move the TreasureChestMaker folder, otherwise it will not function.
//
// If you close the Chest Maker window before saving, you will lose your chest.
//
// All materials created for your chests are stored under /TreasureChestMaker/TCM/ChestMaterials
// You can move them to any folder you wish once they're created.
//
//
  //////////////////////
 //* HOW TO USE TCM *//
//////////////////////
//
//
// Step 1:	TCM is located in the file menu under ChestMaker. Click ChestMaker then select "Make A Chest!"
// 
// Step 2:	You can now give your Chest a name.
//			You won't be able to save your chest unless it has a name.
//				NOTE: Materials are named based on your chest name, so try to use a different chest name each time.
//
// Step 3:	Select your chest Type (Small, Medium or Large), your scene camera will pan over to the chest and you can now start making your chest.
//
// Step 4:	Once you've selected the items and colors for your chest, go ahead and hit SAVE.
//			Again, you'll need a chest name in order to save
//
// Step 5:	Your chest is now created! A script is added to the chest to allow it to be opened.
//			Clicking on the chest will open it. You can change the open Speed as well as allow it to be closed.
//
  /////////////
 //* VIDEO *//
/////////////
//
// TCM: http://youtu.be/uruPC9LnBQM
//
  /////////////////////////////////////////
 //* Not so frequently asked questions *//
/////////////////////////////////////////
//
// 1.)	I created a chest but I want to change the color, where is the material!
//			A)	Materials are located under /TreasureChestMaker/TCM/ChestMaterials
//
// 2.)	I lost my material to my chest, how can I get it back?
//			A)	Under TreasureChestMaker/TCM/Models/Materials you can find the main materials for the models. You can copy the materials
//				as a new material and place them within each object of your chest. Otherwise you can create a new chest.
//
// 3.)	I want to open the chest through my own script, how can I do that?
//			A)	Your chest as an activate chest script with a public boolean called "_open"
//				In your script you can say myChest.GetComponent<ActivateChest>()._open = true;
//
  ///////////////
 //* CONTACT *//
///////////////
//
// E-Mail: mike.desjardins@outlook.com
// Twitter: @ZeroLogics
// Website: www.zerologics.com
//
// I'm fairly active on Twitter and through e-mail, so I'm not too hard to get a hold of.
//
// Thanks you and enjoy!!
//