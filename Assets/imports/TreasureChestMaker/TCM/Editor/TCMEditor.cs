#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace TCM {
	public class TCMEditor : EditorWindow {
		
		private TCMPrefabs tcmPrefabs;
		
		private enum ChestType { None, Small, Medium, Large};
		private ChestType chestType, selectedType;
		
		private string chestName = "";  
		
		private GameObject 	myChest, lid, lidClosed, lidOpen,
							bb02,bb03,bb04,bb05,
							tb02,tb03,tb04,tb05,
							cBasic, cLegs, cSpike, cSpike2,
							handleF, handleS, handleS2,
							tl01, tl02, bl01, bl02,
							spikeBottom01, spikeBottom02, spikeBottom03, spikeBottom03Bent, spikeBottom04, spikeBottom04Bent,
							spikeTop01, spikeTop02, spikeTop03, spikeTop03Bent, spikeTop04, spikeTop04Bent, spikeTop05, 
							spikeTop06, spikeTop07, spikeTop08, spikeTop09, spikeTop10, spikeTop11, spikeTop12, spikeTop12Big;
		
		private Material topBaseMat, topBorderMat, bottomBaseMat, bottomBorderMat, baseMat, borderMat;
		
		private enum NumColors { TwoColors, FourColors };
		private NumColors numColors;
		
		private enum TopBaseMat { Wood, Metal };
		private TopBaseMat topBaseMatSel;
		
		private enum BottomBaseMat { Wood, Metal };
		private BottomBaseMat bottomBaseMatSel;
		
		private enum TopBorderMat { Wood, Metal };
		private TopBorderMat topBorderMatSel;
		
		private enum BottomBorderMat { Wood, Metal };
		private BottomBorderMat bottomBorderMatSel;
		
		
		private enum BaseMat { Wood, Metal };
		private BaseMat baseMatSel;
		private enum BorderMat { Wood, Metal };
		private BorderMat borderMatSel;
		
		private Color 	baseMatCol = Color.white,
				 		borderMatCol = Color.white,
				 		topBorderMatCol = Color.white,
				 		topBaseMatCol = Color.white,
				 		bottomBorderMatCol = Color.white,
				 		bottomBaseMatCol = Color.white;
		
		private string[] borderString = new string[] { "None", "Thin", "Thick" };
		private int topDoubleBorderSelect, topCenterBorderSelect, bottomDoubleBorderSelect, bottomCenterBorderSelect;
		
		private string[] lockString = new string[] { "None", "Angled", "Square" };
		private int bottomLockSelect, topLockSelect;
		
		private string[] spikesBottomString = new string[] {"None", "Small", "Big"};
		private int spikesBottomUpperSelect, spikesBottomLowerSelect;
		
		private string[] cornerString = new string[] {"None", "Basic", "Spikes"};
		private int cornerSelect;
		
		private string[] spikesString = new string[] {"None", "Small", "Big"};
		private int spikeEdgeSet1Sel, spikeEdgeSet2Sel,
					spikeDoubleSet1Sel, spikeDoubleSet2Sel,
					spikeCenterSet1Sel, spikeCenterSet2Sel;
		
		private bool _materialsCreated, 
					 _hasLegs, _addLegSpikes,
					 _handleFront, _handleSide01, _handleSide02,
					 _bendBottomUpperSpike, _bendBottomLowerSpike,
					 _bendTopEdgeSet1, _bendTopEdgeSet2,
					 _bigTopSpike;
					 
		private Vector2 scrollView;
		
		[MenuItem ("ChestMaker/Make a Chest!")]
		static void Init () {
			#pragma warning disable
			TCMEditor window = (TCMEditor)EditorWindow.GetWindow (typeof (TCMEditor));
			window.title = "Chest Maker";
			#pragma warning restore
		}
		
		void OnDisable(){
			DestroyImmediate(myChest);
			AssetDatabase.DeleteAsset("Assets/newTopBaseMaterial.mat");
			AssetDatabase.DeleteAsset("Assets/newTopBorderMaterial.mat");
			AssetDatabase.DeleteAsset("Assets/newBottomBaseMaterial.mat");
			AssetDatabase.DeleteAsset("Assets/newBottomBorderMaterial.mat");
			AssetDatabase.DeleteAsset("Assets/newBaseMaterial.mat");
			AssetDatabase.DeleteAsset("Assets/newBorderMaterial.mat");
		}
		
		void OnGUI () {
			scrollView = EditorGUILayout.BeginScrollView(scrollView);
			Texture2D tcmTitle = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/TreasureChestMaker/TCM/GUITexture/tcmTitle.png", typeof(Texture2D));
			GUI.Label(new Rect(Screen.width/2-150,0,300,167), tcmTitle);
			EditorGUILayout.LabelField("",GUILayout.Height(167));
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Chest Name");
			chestName = EditorGUILayout.TextField(chestName);
			EditorGUILayout.EndHorizontal();
			if(GUILayout.Button("Random")){
				Randomize();
			}
			CheckForChest();
			if(chestType != ChestType.None){
				CheckForMaterials();
				CheckBorders();
				CheckCorners();
				CheckHandles();
	            CheckLock();
				CheckBottomSpikes();
	            CheckTopSpikes();
	            SaveChest();
	        }
			EditorGUILayout.EndScrollView();
		}
		
		private void SetupChest(string chestName){
			GameObject tempChest = new GameObject();
			tempChest.AddComponent<TCMPrefabs>();
			if(chestType == ChestType.Small){
				SpawnChest(tempChest.GetComponent<TCMPrefabs>().chestSmall);
			} else if(chestType == ChestType.Medium){
				SpawnChest(tempChest.GetComponent<TCMPrefabs>().chestMedium);
			} else if(chestType == ChestType.Large){
				SpawnChest(tempChest.GetComponent<TCMPrefabs>().chestLarge);
			}
			DestroyImmediate(tempChest);
			myChest.AddComponent<TCMPrefabs>();
			tcmPrefabs = myChest.GetComponent<TCMPrefabs>();
		}
		
		private void SpawnChest(GameObject newChest){
			myChest = PrefabUtility.InstantiatePrefab(newChest) as GameObject;
			PrefabUtility.DisconnectPrefabInstance(myChest);
			myChest.transform.position = Vector3.zero;
			myChest.transform.rotation = Quaternion.Euler(-90,180,0);
			myChest.transform.parent = myChest.transform;
			DisableAddons();
			Selection.activeGameObject = myChest;
			SceneView.lastActiveSceneView.FrameSelected();
		}
		
		private void DisableAddons(){
			foreach(Transform chestPiece in myChest.transform){
				if(chestPiece.name == "Closed"){
					lidClosed = chestPiece.gameObject;
				}
				if(chestPiece.name == "Open"){
					lidOpen = chestPiece.gameObject;
				}
				if(chestPiece.name == "Top"){
					lid = chestPiece.gameObject;
				}
				foreach(Transform addon in chestPiece){
					if(addon.name != "add_BorderBottom" & addon.name != "add_BorderTop" & addon.name != "Lid"){
						addon.gameObject.SetActive(false);
						if(addon.name == "add_BorderBottom02"){
							bb02 = addon.gameObject;
						}
						if(addon.name == "add_BorderBottom03"){
							bb03 = addon.gameObject;
	                    }
						if(addon.name == "add_BorderBottom04"){
							bb04 = addon.gameObject;
	                    }
						if(addon.name == "add_BorderBottom05"){
							bb05 = addon.gameObject;
	                    }
						if(addon.name == "add_BorderTop02"){
							tb02 = addon.gameObject;
						}
						if(addon.name == "add_BorderTop03"){
							tb03 = addon.gameObject;
	                    }
						if(addon.name == "add_BorderTop04"){
							tb04 = addon.gameObject;
	                    }
						if(addon.name == "add_BorderTop05"){
							tb05 = addon.gameObject;
	                    }
	                    if(addon.name == "add_CornersBasic"){
	                    	cBasic = addon.gameObject;
	                    }
						if(addon.name == "add_CornersLegs"){
							cLegs = addon.gameObject;
						}
						if(addon.name == "add_CornersSpikes"){
							cSpike = addon.gameObject;
						}
						if(addon.name == "add_CornersSpikes2"){
							cSpike2 = addon.gameObject;
						}
						if(addon.name == "add_HandlesFront"){
							handleF = addon.gameObject;
						}
						if(addon.name == "add_HandlesSide01"){
							handleS = addon.gameObject;
						}
						if(addon.name == "add_HandlesSide02"){
							handleS2 = addon.gameObject;
						}
						if(addon.name == "add_LockTop01"){
							tl01 = addon.gameObject;
						}
						if(addon.name == "add_LockTop02"){
							tl02 = addon.gameObject;
						}
						if(addon.name == "add_LockBottom01"){
							bl01 = addon.gameObject;
						}
						if(addon.name == "add_LockBottom02"){
							bl02 = addon.gameObject;
						}
						/* SPIKES BOTTOM */
						if(addon.name == "add_SpikesBottom01"){
							spikeBottom01 = addon.gameObject;
						}
						if(addon.name == "add_SpikesBottom02"){
							spikeBottom02 = addon.gameObject;
						}
						if(addon.name == "add_SpikesBottom03"){
							spikeBottom03 = addon.gameObject;
						}
						if(addon.name == "add_SpikesBottom03Bent"){
							spikeBottom03Bent = addon.gameObject;
						}
						if(addon.name == "add_SpikesBottom04"){
							spikeBottom04 = addon.gameObject;
						}
						if(addon.name == "add_SpikesBottom04Bent"){
							spikeBottom04Bent = addon.gameObject;
						}
						/* SPIKES TOP */
						if(addon.name == "add_Spikes01"){
							spikeTop01 = addon.gameObject;
						}
						if(addon.name == "add_Spikes02"){
							spikeTop02 = addon.gameObject;
						}
						if(addon.name == "add_Spikes03"){
							spikeTop03 = addon.gameObject;
						}
						if(addon.name == "add_Spikes03Bent"){
							spikeTop03Bent = addon.gameObject;
						}
						if(addon.name == "add_Spikes04"){
							spikeTop04 = addon.gameObject;
						}
						if(addon.name == "add_Spikes04Bent"){
							spikeTop04Bent = addon.gameObject;
						}
						if(addon.name == "add_Spikes05"){
							spikeTop05 = addon.gameObject;
						}
						if(addon.name == "add_Spikes06"){
							spikeTop06 = addon.gameObject;
						}
						if(addon.name == "add_Spikes07"){
							spikeTop07 = addon.gameObject;
						}
						if(addon.name == "add_Spikes08"){
							spikeTop08 = addon.gameObject;
						}
						if(addon.name == "add_Spikes09"){
							spikeTop09 = addon.gameObject;
						}
						if(addon.name == "add_Spikes10"){
							spikeTop10 = addon.gameObject;
						}
						if(addon.name == "add_Spikes11"){
							spikeTop11 = addon.gameObject;
						}
						if(addon.name == "add_Spikes12"){
							spikeTop12 = addon.gameObject;
						}
						if(addon.name == "add_Spikes12Big"){
							spikeTop12Big = addon.gameObject;
						}
	                }
	            }
	        }
	    }
		
		private void CheckForChest(){
			chestType = (ChestType)EditorGUILayout.EnumPopup("Chest Type", chestType);
			if(myChest){
				if(chestType != selectedType){
					DestroyImmediate(myChest);
				}
			}
			selectedType = chestType;
			if(!myChest){
				if(chestType == ChestType.Small){
					SetupChest("Small");
				} else if(chestType == ChestType.Medium){
					SetupChest("Medium");
				} else if(chestType == ChestType.Large){
					SetupChest("Large");
				} else {
					DestroyImmediate(myChest);
				}
			}
		}
		
		private void CheckForMaterials(){
			numColors = (NumColors)EditorGUILayout.EnumPopup("Number of Colors", numColors);
			if(numColors == NumColors.TwoColors){
				EditorGUILayout.BeginHorizontal();
				baseMatSel = (BaseMat)EditorGUILayout.EnumPopup("Base Material", baseMatSel);
				baseMatCol = EditorGUILayout.ColorField(baseMatCol);
				EditorGUILayout.EndHorizontal();
				
				EditorGUILayout.BeginHorizontal();
				borderMatSel = (BorderMat)EditorGUILayout.EnumPopup("Border Material", borderMatSel);
				borderMatCol = EditorGUILayout.ColorField(borderMatCol);
				EditorGUILayout.EndHorizontal();
				
				if(!baseMat){
					baseMat = new Material(Shader.Find("Diffuse"));
					AssetDatabase.CreateAsset(baseMat, "Assets/newBaseMaterial.mat");
				}
				if(!borderMat){
					borderMat = new Material(Shader.Find("Diffuse"));
					AssetDatabase.CreateAsset(borderMat, "Assets/newBorderMaterial.mat");
				}
	
				if(baseMatSel == BaseMat.Wood){
					if(baseMat.mainTexture != tcmPrefabs.baseWood){
						baseMat.mainTexture = tcmPrefabs.baseWood;
					}
				} else {
					if(baseMat.mainTexture != tcmPrefabs.baseMetal){
						baseMat.mainTexture = tcmPrefabs.baseMetal;
					}
				}
				if(borderMatSel == BorderMat.Wood){
					if(borderMat.mainTexture != tcmPrefabs.borderWood){
						borderMat.mainTexture = tcmPrefabs.borderWood;
					}
				} else {
					if(borderMat.mainTexture != tcmPrefabs.borderMetal){
						borderMat.mainTexture = tcmPrefabs.borderMetal;
					}
				}
				AddMaterials(baseMat,borderMat,baseMat,borderMat,baseMatCol,borderMatCol,baseMatCol,borderMatCol);
			} else {
				EditorGUILayout.BeginHorizontal();
				topBaseMatSel = (TopBaseMat)EditorGUILayout.EnumPopup("Top Base Material", topBaseMatSel);
				topBaseMatCol = EditorGUILayout.ColorField(topBaseMatCol);
				EditorGUILayout.EndHorizontal();
				
				EditorGUILayout.BeginHorizontal();
				topBorderMatSel = (TopBorderMat)EditorGUILayout.EnumPopup("Top Border Material", topBorderMatSel);
				topBorderMatCol = EditorGUILayout.ColorField(topBorderMatCol);
				EditorGUILayout.EndHorizontal();
				
				EditorGUILayout.BeginHorizontal();
				bottomBaseMatSel = (BottomBaseMat)EditorGUILayout.EnumPopup("Bottom Base Material", bottomBaseMatSel);
				bottomBaseMatCol = EditorGUILayout.ColorField(bottomBaseMatCol);
				EditorGUILayout.EndHorizontal();
				
				EditorGUILayout.BeginHorizontal();
				bottomBorderMatSel = (BottomBorderMat)EditorGUILayout.EnumPopup("Bottom Border Material", bottomBorderMatSel);
				bottomBorderMatCol = EditorGUILayout.ColorField(bottomBorderMatCol);
				EditorGUILayout.EndHorizontal();
				
				if(!topBaseMat){
					topBaseMat = new Material(Shader.Find("Diffuse"));
					AssetDatabase.CreateAsset(topBaseMat, "Assets/newTopBaseMaterial.mat");
				}
				if(!topBorderMat){
					topBorderMat = new Material(Shader.Find("Diffuse"));
					AssetDatabase.CreateAsset(topBorderMat, "Assets/newTopBorderMaterial.mat");
				}
				if(!bottomBaseMat){
					bottomBaseMat = new Material(Shader.Find("Diffuse"));
					AssetDatabase.CreateAsset(bottomBaseMat, "Assets/newBottomBaseMaterial.mat");
				}
				if(!bottomBorderMat){
					bottomBorderMat = new Material(Shader.Find("Diffuse"));
					AssetDatabase.CreateAsset(bottomBorderMat, "Assets/newBottomBorderMaterial.mat");
				}
				
				if(topBaseMatSel == TopBaseMat.Wood){
					if(topBaseMat.mainTexture != tcmPrefabs.baseWood){
						topBaseMat.mainTexture = tcmPrefabs.baseWood;
					}
				} else {
					if(topBaseMat.mainTexture != tcmPrefabs.baseMetal){
						topBaseMat.mainTexture = tcmPrefabs.baseMetal;
					}
				}
				
				if(topBorderMatSel == TopBorderMat.Wood){
					if(topBorderMat.mainTexture != tcmPrefabs.borderWood){
						topBorderMat.mainTexture = tcmPrefabs.borderWood;
					}
				} else {
					if(topBorderMat.mainTexture != tcmPrefabs.borderMetal){
						topBorderMat.mainTexture = tcmPrefabs.borderMetal;
					}
				}
				
				
				
				
				if(bottomBaseMatSel == BottomBaseMat.Wood){
					if(bottomBaseMat.mainTexture != tcmPrefabs.baseWood){
						bottomBaseMat.mainTexture = tcmPrefabs.baseWood;
					}
				} else {
					if(bottomBaseMat.mainTexture != tcmPrefabs.baseMetal){
						bottomBaseMat.mainTexture = tcmPrefabs.baseMetal;
					}
				}
				
				if(bottomBorderMatSel == BottomBorderMat.Wood){
					if(bottomBorderMat.mainTexture != tcmPrefabs.borderWood){
						bottomBorderMat.mainTexture = tcmPrefabs.borderWood;
					}
				} else {
					if(bottomBorderMat.mainTexture != tcmPrefabs.borderMetal){
						bottomBorderMat.mainTexture = tcmPrefabs.borderMetal;
					}
				}
				AddMaterials(topBaseMat,topBorderMat,bottomBaseMat,bottomBorderMat,topBaseMatCol,topBorderMatCol,bottomBaseMatCol,bottomBorderMatCol);
			}
		}
			
		private void AddMaterials(Material topBase, Material topBorder,Material bottomBase,Material bottomBorder,Color topBaseC,Color topBorderC,Color bottomBaseC,Color bottomBorderC){
		
			if(borderMat && borderMat.color != topBorderC){
				borderMat.color = topBorderC;
	        }
			if(baseMat && baseMat.color != topBaseC){
				baseMat.color = topBaseC;
	        }
	        
	        if(topBorderMat && topBorderMat.color != topBorderC){
				topBorderMat.color = topBorderC;
			}
			if(topBaseMat && topBaseMat.color != topBaseC){
				topBaseMat.color = topBaseC;
			}
			if(bottomBorderMat && bottomBorderMat.color != bottomBorderC){
				bottomBorderMat.color = bottomBorderC;
			}
			if(bottomBaseMat && bottomBaseMat.color != bottomBaseC){
				bottomBaseMat.color = bottomBaseC;
			}
			
			foreach(Transform chestPiece in myChest.transform){
				if(chestPiece.name == "Top"){
					foreach(Transform piece in chestPiece){
						if(piece.name != "Lid"){
							piece.gameObject.GetComponent<Renderer>().material = topBorder;
						} else {
								piece.gameObject.GetComponent<Renderer>().material = topBase;
						}
					}
				}
				if(chestPiece.name == "Bottom"){
					chestPiece.gameObject.GetComponent<Renderer>().material = bottomBase;
					foreach(Transform piece in chestPiece){
						piece.gameObject.GetComponent<Renderer>().material = bottomBorder;
					}
				}
			}
		}
		
		private void CheckBorders()
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Top Double Borders");
			topDoubleBorderSelect = GUILayout.SelectionGrid(topDoubleBorderSelect,borderString,3);
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Top Center Border");
			topCenterBorderSelect = GUILayout.SelectionGrid(topCenterBorderSelect,borderString,3);
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Bottom Double Borders");
			bottomDoubleBorderSelect = GUILayout.SelectionGrid(bottomDoubleBorderSelect,borderString,3);
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Bottom Center Border");
			bottomCenterBorderSelect = GUILayout.SelectionGrid(bottomCenterBorderSelect,borderString,3);
	        EditorGUILayout.EndHorizontal();
	        
	        if(bb02 && bb03 && bb04 && bb05 && tb02 && tb03 && tb04 && tb05)
			{
				if(topDoubleBorderSelect == 1){
					tb02.SetActive(true);
					tb05.SetActive(false);
				}
				else if(topDoubleBorderSelect == 2){
					tb05.SetActive(true);
					tb02.SetActive(false);
				} else {
					tb02.SetActive(false);
					tb05.SetActive(false);
				}
				
				if(topCenterBorderSelect == 1){
					tb03.SetActive(true);
					tb04.SetActive(false);
				}
				else if(topCenterBorderSelect == 2){
					tb04.SetActive(true);
					tb03.SetActive(false);
				} else {
		            tb03.SetActive(false);
		            tb04.SetActive(false);
		        }
		        
				if(bottomDoubleBorderSelect == 1){
					bb02.SetActive(true);
					bb05.SetActive(false);
				}
				else if(bottomDoubleBorderSelect == 2){
					bb05.SetActive(true);
					bb02.SetActive(false);
				} else {
					bb02.SetActive(false);
	                bb05.SetActive(false);
	            }
	            
				if(bottomCenterBorderSelect == 1){
					bb03.SetActive(true);
					bb04.SetActive(false);
				}
				else if(bottomCenterBorderSelect == 2){
					bb04.SetActive(true);
					bb03.SetActive(false);
				} else {
					bb03.SetActive(false);
	                bb04.SetActive(false);
	            }
	        }
	    }
	    
	    private void CheckCorners(){
	    	if(!_hasLegs){
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("Bottom Corners");
	    		cornerSelect = GUILayout.SelectionGrid(cornerSelect,cornerString,3);
				EditorGUILayout.EndHorizontal();
	    	}
			_hasLegs = EditorGUILayout.Toggle("Add Legs",_hasLegs);
			
			if(cBasic && cLegs && cSpike && cSpike2){
	    		if(_hasLegs){
	    			cLegs.SetActive(true);
	    			cSpike.SetActive(false);
	    			cBasic.SetActive(false);
	    			myChest.transform.position = new Vector3(0,0.315f,0);
					_addLegSpikes = EditorGUILayout.Toggle("Add Leg Spikes",_addLegSpikes);
					if(_addLegSpikes){
						cSpike2.SetActive(true);
					} else {
						cSpike2.SetActive(false);
					}
	            } else {
	            	if(cornerSelect == 1){
						cBasic.SetActive(true);
						cSpike.SetActive(false);
					} else if(cornerSelect == 2){
						cSpike.SetActive(true);
						cBasic.SetActive(false);
					} else {
						cBasic.SetActive(false);
						cSpike.SetActive(false);
					}
					cLegs.SetActive(false);
					cSpike2.SetActive(false);
	                myChest.transform.position = new Vector3(0,0,0);
	            }
	    	}
	    }
	    
	    private void CheckHandles(){
	    	_handleFront = EditorGUILayout.Toggle("Add Front Handles", _handleFront);
			_handleSide02 = EditorGUILayout.Toggle("Add Side Handles", _handleSide02);
			_handleSide01 = EditorGUILayout.Toggle("Add Side Rings", _handleSide01);
	    
	    	if(handleF && handleS && handleS2){
	    		if(_handleFront){
	    			handleF.gameObject.SetActive(true);
	    		} else {
					handleF.gameObject.SetActive(false);
	    		}
	    		if(_handleSide01){
	    			handleS.gameObject.SetActive(true);
	    		} else {
					handleS.gameObject.SetActive(false);
	    		}
	    		if(_handleSide02){
	    			handleS2.gameObject.SetActive(true);
	    		} else {
					handleS2.gameObject.SetActive(false);
	    		}
	    	}
	    }
	    
	    private void CheckLock(){
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Bottom Lock");
			bottomLockSelect = GUILayout.SelectionGrid(bottomLockSelect,lockString,3);
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Top Lock");
			topLockSelect = GUILayout.SelectionGrid(topLockSelect,lockString,3);
			EditorGUILayout.EndHorizontal();
	    
	    	if(tl01 && tl02 && bl01 && bl02){
	    		if(bottomLockSelect == 1){
					bl01.SetActive(true);
					bl02.SetActive(false);
	    		} else if (bottomLockSelect == 2){
					bl02.SetActive(true);
					bl01.SetActive(false);
	    		} else {
	    			bl01.SetActive(false);
					bl02.SetActive(false);
	    		}
	    		
				if(topLockSelect == 1){
					tl01.SetActive(true);
					tl02.SetActive(false);
				} else if (topLockSelect == 2){
					tl02.SetActive(true);
					tl01.SetActive(false);
				} else {
					tl01.SetActive(false);
					tl02.SetActive(false);
				}
	    	}
	    }
		
	    private void CheckBottomSpikes(){
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Spikes Bottom Upper");
			spikesBottomUpperSelect = GUILayout.SelectionGrid(spikesBottomUpperSelect,spikesBottomString,3);
			EditorGUILayout.EndHorizontal();
			
			if(spikesBottomUpperSelect == 2){
				_bendBottomUpperSpike = EditorGUILayout.Toggle("Bend Spike",_bendBottomUpperSpike);
			}
	    
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Spikes Bottom Lower");
			spikesBottomLowerSelect = GUILayout.SelectionGrid(spikesBottomLowerSelect,spikesBottomString,3);
			EditorGUILayout.EndHorizontal();
			
			if(spikesBottomLowerSelect == 2){
				_bendBottomLowerSpike = EditorGUILayout.Toggle("Bend Spike",_bendBottomLowerSpike);
			}
	    
	    	if(spikeBottom01 && spikeBottom02 && spikeBottom03 && spikeBottom03Bent && spikeBottom04 && spikeBottom04Bent){
	    		if(spikesBottomUpperSelect == 1){
					spikeBottom01.SetActive(true);
					spikeBottom03.SetActive(false);
					spikeBottom03Bent.SetActive(false);
	    		} else if(spikesBottomUpperSelect == 2){
	    			if(_bendBottomUpperSpike){
						spikeBottom03Bent.SetActive(true);
						spikeBottom03.SetActive(false);
						
	    			} else {
						spikeBottom03.SetActive(true);
						spikeBottom03Bent.SetActive(false);
	    			}
					spikeBottom01.SetActive(false);
					
	    		} else {
	    			spikeBottom01.SetActive(false);
					spikeBottom03.SetActive(false);
					spikeBottom03Bent.SetActive(false);
	    		}
	    		/* LOWER SPIKES */
	    		if(!cSpike.activeSelf){
					if(spikesBottomLowerSelect == 1){
						spikeBottom02.SetActive(true);
						spikeBottom04.SetActive(false);
						spikeBottom04Bent.SetActive(false);
					} else if(spikesBottomLowerSelect == 2){
						if(_bendBottomLowerSpike){
							spikeBottom04Bent.SetActive(true);
							spikeBottom04.SetActive(false);
							
						} else {
							spikeBottom04.SetActive(true);
							spikeBottom04Bent.SetActive(false);
						}
						spikeBottom02.SetActive(false);
					} else {
						spikeBottom02.SetActive(false);
						spikeBottom04.SetActive(false);
						spikeBottom04Bent.SetActive(false);
					}
				} else {
					spikeBottom02.SetActive(false);
					spikeBottom04.SetActive(false);
					spikeBottom04Bent.SetActive(false);
				}
	    	}
	    }
	
	    private void CheckTopSpikes(){
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Spikes Top Edge Set 1");
			spikeEdgeSet1Sel = GUILayout.SelectionGrid(spikeEdgeSet1Sel,spikesString,3);
			EditorGUILayout.EndHorizontal();
			
			if(spikeEdgeSet1Sel == 2){
				_bendTopEdgeSet1 = EditorGUILayout.Toggle("Bend Spike",_bendTopEdgeSet1);
			}
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Spikes Top Edge Set 2");
			spikeEdgeSet2Sel = GUILayout.SelectionGrid(spikeEdgeSet2Sel,spikesString,3);
			EditorGUILayout.EndHorizontal();
			
			if(spikeEdgeSet2Sel == 2){
				_bendTopEdgeSet2 = EditorGUILayout.Toggle("Bend Spike",_bendTopEdgeSet2);
			}
	    
	    if(spikeTop01 && spikeTop02 && spikeTop03 && spikeTop03Bent && spikeTop04 && spikeTop04Bent && spikeTop05 &&
			spikeTop06 && spikeTop07 && spikeTop08 && spikeTop09 && spikeTop10 && spikeTop11 && spikeTop12 && spikeTop12Big){
				/* EDGE SET 1 */
				if(spikeEdgeSet1Sel == 1){
					spikeTop01.SetActive(true);
					spikeTop03.SetActive(false);
					spikeTop03Bent.SetActive(false);
				} else if(spikeEdgeSet1Sel == 2){
					if(_bendTopEdgeSet1){
						spikeTop03Bent.SetActive(true);
						spikeTop03.SetActive(false);
					} else {
						spikeTop03.SetActive(true);
						spikeTop03Bent.SetActive(false);
					}
					spikeTop01.SetActive(false);
				} else {
					spikeTop01.SetActive(false);
					spikeTop03.SetActive(false);
					spikeTop03Bent.SetActive(false);
				}
				/* EDGE SET 2 */
				if(spikeEdgeSet2Sel == 1){
					spikeTop02.SetActive(true);
					spikeTop04.SetActive(false);
					spikeTop04Bent.SetActive(false);
				} else if(spikeEdgeSet2Sel == 2){
					if(_bendTopEdgeSet2){
						spikeTop04Bent.SetActive(true);
						spikeTop04.SetActive(false);
					} else {
						spikeTop04.SetActive(true);
						spikeTop04Bent.SetActive(false);
					}
					spikeTop02.SetActive(false);
				} else {
					spikeTop02.SetActive(false);
					spikeTop04.SetActive(false);
					spikeTop04Bent.SetActive(false);
				}
				
				
				/* DOUBLE SPIKES */
				if(tb02.activeSelf || tb05.activeSelf){
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField("Spikes Top Double Set 1");
					spikeDoubleSet1Sel = GUILayout.SelectionGrid(spikeDoubleSet1Sel,spikesString,3);
					EditorGUILayout.EndHorizontal();
					
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField("Spikes Top Double Set 2");
					spikeDoubleSet2Sel = GUILayout.SelectionGrid(spikeDoubleSet2Sel,spikesString,3);
					EditorGUILayout.EndHorizontal();
					
					/* DOUBLE SET 1 */
					if(spikeDoubleSet1Sel == 1){
						spikeTop05.SetActive(true);
						spikeTop07.SetActive(false);
					} else if(spikeDoubleSet1Sel == 2){
						spikeTop05.SetActive(false);
						spikeTop07.SetActive(true);
					}else {
						spikeTop05.SetActive(false);
						spikeTop07.SetActive(false);
					}
					/*DOUBLE SET 2 */
					if(spikeDoubleSet2Sel == 1){
						spikeTop06.SetActive(true);
						spikeTop08.SetActive(false);
					} else if(spikeDoubleSet2Sel == 2){
						spikeTop06.SetActive(false);
						spikeTop08.SetActive(true);
					}else {
						spikeTop06.SetActive(false);
						spikeTop08.SetActive(false);
					}
					
				} else {
					spikeTop05.SetActive(false);
					spikeTop06.SetActive(false);
					spikeTop07.SetActive(false);	
					spikeTop08.SetActive(false);			
				}
				
				/* CENTER SPIKES */
				if(tb03.activeSelf || tb04.activeSelf){
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField("Spikes Top Center Set 1");
					spikeCenterSet1Sel = GUILayout.SelectionGrid(spikeCenterSet1Sel,spikesString,3);
					EditorGUILayout.EndHorizontal();
					
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField("Spikes Top Center Set 2");
					spikeCenterSet2Sel = GUILayout.SelectionGrid(spikeCenterSet2Sel,spikesString,3);
					EditorGUILayout.EndHorizontal();
					
					if(spikeCenterSet2Sel == 2 && tb04.activeSelf){
						_bigTopSpike = EditorGUILayout.Toggle("Big Spike",_bigTopSpike);
					}
				
					/* CENTER SET 1 */
					if(spikeCenterSet1Sel == 1){
						spikeTop09.SetActive(true);
						spikeTop11.SetActive(false);	
					} else if(spikeCenterSet1Sel == 2){
						spikeTop09.SetActive(false);
						spikeTop11.SetActive(true);	
					} else {
						spikeTop09.SetActive(false);
						spikeTop11.SetActive(false);	
					}
					/* CENTER SET 2 */
					if(spikeCenterSet2Sel == 1){
						spikeTop10.SetActive(true);
						spikeTop12.SetActive(false);	
					} else if(spikeCenterSet2Sel == 2){
						if(_bigTopSpike && tb04.activeSelf){
							spikeTop12.SetActive(false);	
							spikeTop12Big.SetActive(true);
						} else {
							spikeTop12.SetActive(true);	
							spikeTop12Big.SetActive(false);
						}
						spikeTop10.SetActive(false);
					} else {
						spikeTop10.SetActive(false);
						spikeTop12.SetActive(false);
						spikeTop12Big.SetActive(false);	
					}
				} else {
					spikeTop09.SetActive(false);
					spikeTop10.SetActive(false);
					spikeTop11.SetActive(false);
					spikeTop12.SetActive(false);
					spikeTop12Big.SetActive(false);
				}
			}
	    }
	    
	    private void SaveChest(){
	    	if(GUILayout.Button("Save Chest")){
	    		if(chestName == ""){
	    			EditorUtility.DisplayDialog("Missing Chest Name", "Please give your chest a name.", "OK");
	    		} else {
	    			if(EditorUtility.DisplayDialog("Save Chest", "Are you sure you want to save this chest?","Yes","No")){
	    				SaveMaterials();
	    				DeleteDisabledAddons();
	    				myChest.name = chestName;
						DestroyImmediate(myChest.GetComponent<TCMPrefabs>());
						ActivateChest animChest = myChest.AddComponent<ActivateChest>();
	    				animChest.lid = lid.transform;
	    				animChest.lidOpen = lidOpen.transform;
	    				animChest.lidClose = lidClosed.transform;
	    				myChest = null;
	    				/* Close Window*/
	    				Close ();
						#pragma warning disable
						TCMEditor window = (TCMEditor)EditorWindow.GetWindow (typeof (TCMEditor));
						#pragma warning restore
						
	    			}
	    		}
	    	}
	    }
	    
	    private void SaveMaterials(){
	    	if(numColors == NumColors.TwoColors){
				AssetDatabase.MoveAsset("Assets/newBaseMaterial.mat","Assets/TreasureChestMaker/TCM/ChestMaterials/" + chestName + "BaseMaterial.mat");
				AssetDatabase.MoveAsset("Assets/newBorderMaterial.mat","Assets/TreasureChestMaker/TCM/ChestMaterials/" + chestName + "BorderMaterial.mat");
				AssetDatabase.DeleteAsset("Assets/newTopBaseMaterial.mat");
				AssetDatabase.DeleteAsset("Assets/newTopBorderMaterial.mat");
				AssetDatabase.DeleteAsset("Assets/newBottomBaseMaterial.mat");
				AssetDatabase.DeleteAsset("Assets/newBottomBorderMaterial.mat");
	    	} else {
				AssetDatabase.MoveAsset("Assets/newTopBaseMaterial.mat","Assets/TreasureChestMaker/TCM/ChestMaterials/" + chestName + "TopBaseMaterial.mat");
				AssetDatabase.MoveAsset("Assets/newTopBorderMaterial.mat","Assets/TreasureChestMaker/TCM/ChestMaterials/" + chestName + "TopBorderMaterial.mat");
				AssetDatabase.MoveAsset("Assets/newBottomBaseMaterial.mat","Assets/TreasureChestMaker/TCM/ChestMaterials/" + chestName + "BottomBaseMaterial.mat");
				AssetDatabase.MoveAsset("Assets/newBottomBorderMaterial.mat","Assets/TreasureChestMaker/TCM/ChestMaterials/" + chestName + "BottomBorderMaterial.mat");
				AssetDatabase.DeleteAsset("Assets/newBaseMaterial.mat");
				AssetDatabase.DeleteAsset("Assets/newBorderMaterial.mat");
	    	}
	    }
	    
	    private void DeleteDisabledAddons(){
	    	List<Transform> disabledAddons = new List<Transform>();
			foreach(Transform chestPiece in myChest.transform){
				foreach(Transform addon in chestPiece){
					if(!addon.gameObject.activeSelf){
						disabledAddons.Add(addon);
					}
				}
			}
			disabledAddons.ForEach(child => DestroyImmediate(child.gameObject));
	    }
	    
	    private void Randomize(){
	    
	    	// Chest Type
	    	int randChestType = Random.Range(0,3);
	    	switch(randChestType){
	    		case 0:
	    			chestType = ChestType.Small;
	    			break;
	    		case 1:
	    			chestType = ChestType.Medium;
	    			break;
	    		case 2:
	    			chestType = ChestType.Large;
	    			break;
	    		default:
	    			break;
	    	}
	    	
	    	// Chest Colors & Materials
	    	int randNumColors = Random.Range(0,2);
	    	switch(randNumColors){
	    		case 0:
					int randBaseMat = Random.Range(0,2);
					bool _randBaseMat;
					if(randBaseMat == 0) _randBaseMat = true; else _randBaseMat = false;
					baseMatSel = (_randBaseMat) ? BaseMat.Wood : BaseMat.Metal;
					
					int randBorderMat = Random.Range(0,2);
					bool _randBorderMat;
					if(randBorderMat == 0) _randBorderMat = true; else _randBorderMat = false;
					borderMatSel = (_randBorderMat) ? BorderMat.Wood : BorderMat.Metal;
	    		
	    			numColors = NumColors.TwoColors;
					baseMatCol = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));
					borderMatCol = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));
	    			break;
	    		case 1:
	    		
					int randTopBaseMat = Random.Range(0,2);
					bool _randTopBaseMat;
					if(randTopBaseMat == 0) _randTopBaseMat = true; else _randTopBaseMat = false;
					topBaseMatSel = (_randTopBaseMat) ? TopBaseMat.Wood : TopBaseMat.Metal;
					
					int randTopBorderMat = Random.Range(0,2);
					bool _randTopBorderMat;
					if(randTopBorderMat == 0) _randTopBorderMat = true; else _randTopBorderMat = false;
					topBorderMatSel = (_randTopBorderMat) ? TopBorderMat.Wood : TopBorderMat.Metal;
					
					int randBottomBaseMat = Random.Range(0,2);
					bool _randBottomBaseMat;
					if(randBottomBaseMat == 0) _randBottomBaseMat = true; else _randBottomBaseMat = false;
					bottomBaseMatSel = (_randBottomBaseMat) ? BottomBaseMat.Wood : BottomBaseMat.Metal;
				
					int randBottomBorderMat = Random.Range(0,2);
					bool _randBottomBorderMat;
					if(randBottomBorderMat == 0) _randBottomBorderMat = true; else _randBottomBorderMat = false;
					bottomBorderMatSel = (_randBottomBorderMat) ? BottomBorderMat.Wood : BottomBorderMat.Metal;
	    		
	    			numColors = NumColors.FourColors;
					topBaseMatCol = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));
					topBorderMatCol = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));
					bottomBaseMatCol = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));
					bottomBorderMatCol = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));
	    			break;
	    		default:
	    			break;
	    	}
	    	
	    	// Borders
	    	int randTopDoubleBorders = Random.Range(0,3);
			switch(randTopDoubleBorders){
			case 0:
				topDoubleBorderSelect = 0;
				break;
			case 1:
				topDoubleBorderSelect = 1;
				break;
			case 2:
				topDoubleBorderSelect = 2;
				break;
			default:
				break;
			}
			int randTopCenterBorders = Random.Range(0,3);
			switch(randTopCenterBorders){
			case 0:
				topCenterBorderSelect = 0;
				break;
			case 1:
				topCenterBorderSelect = 1;
				break;
			case 2:
				topCenterBorderSelect = 2;
				break;
			default:
				break;
			}
			int randBottomDoubleBorders = Random.Range(0,3);
			switch(randBottomDoubleBorders){
			case 0:
				bottomDoubleBorderSelect = 0;
				break;
			case 1:
				bottomDoubleBorderSelect = 1;
				break;
			case 2:
				bottomDoubleBorderSelect = 2;
				break;
			default:
				break;
			}
			int randBottomCenterBorders = Random.Range(0,3);
			switch(randBottomCenterBorders){
			case 0:
				bottomCenterBorderSelect = 0;
				break;
			case 1:
				bottomCenterBorderSelect = 1;
				break;
			case 2:
				bottomCenterBorderSelect = 2;
				break;
			default:
				break;
			}
			
			// Corners
			int randCorners = Random.Range(0,3);
			switch(randCorners){
			case 0:
				cornerSelect = 0;
				break;
			case 1:
				cornerSelect = 1;
				break;
			case 2:
				cornerSelect = 2;
				break;
			default:
				break;
				
			}
	    	
	    	// Legs
	    	int randLegs = Random.Range(0,2);
	    	bool _randLegs;
	    	if(randLegs == 0) _randLegs = true; else _randLegs = false;
	    	_hasLegs = (_randLegs) ? true : false;
	    	if(_hasLegs){
	    		cornerSelect = 0;
	    		int randLegSpikes = Random.Range(0,2);
				bool _randLegSpikes;
				if(randLegSpikes == 0) _randLegSpikes = true; else _randLegSpikes = false;
				_addLegSpikes = (_randLegSpikes) ? true : false;
	    	}
	    	
	    	// Handles
			int randFrontHandles = Random.Range(0,2);
			bool _randFrontHandles;
			if(randFrontHandles == 0) _randFrontHandles = true; else _randFrontHandles = false;
			_handleFront = (_randFrontHandles) ? true : false;
	    	
			int randSideHandles1 = Random.Range(0,2);
			bool _randSideHandles1;
			if(randSideHandles1 == 0) _randSideHandles1 = true; else _randSideHandles1 = false;
			_handleSide01 = (_randSideHandles1) ? true : false;
			
			int randSideHandles2 = Random.Range(0,2);
			bool _randSideHandles2;
			if(randSideHandles2 == 0) _randSideHandles2 = true; else _randSideHandles2 = false;
			_handleSide02 = (_randSideHandles2) ? true : false;
	    	
	    	
	    	// LOCKS
			int randBottomLock = Random.Range(0,3);
			switch(randBottomLock){
			case 0:
				bottomLockSelect = 0;
				break;
			case 1:
				bottomLockSelect = 1;
				break;
			case 2:
				bottomLockSelect = 2;
				break;
			default:
				break;
			}
			
			int randTopLock = Random.Range(0,3);
			switch(randTopLock){
			case 0:
				topLockSelect = 0;
				break;
			case 1:
				topLockSelect = 1;
				break;
			case 2:
				topLockSelect = 2;
				break;
			default:
				break;
			}
			
			// SPIKES
			
			int randSpikesBottomUpper = Random.Range(0,3);
			switch(randSpikesBottomUpper){
			case 0:
				spikesBottomUpperSelect = 0;
				break;
			case 1:
				spikesBottomUpperSelect = 1;
				break;
			case 2:
				spikesBottomUpperSelect = 2;
				int randSpikesBend = Random.Range(0,2);
				bool _randSpikesBend;
				if(randSpikesBend == 0) _randSpikesBend = true; else _randSpikesBend = false;
				_bendBottomUpperSpike = (_randSpikesBend) ? true : false;
				break;
			default:
				break;
			}
			
			int randSpikesBottomLower = Random.Range(0,3);
			switch(randSpikesBottomLower){
			case 0:
				spikesBottomLowerSelect = 0;
				break;
			case 1:
				spikesBottomLowerSelect = 1;
				break;
			case 2:
				spikesBottomLowerSelect = 2;
				int randSpikesBend = Random.Range(0,2);
				bool _randSpikesBend;
				if(randSpikesBend == 0) _randSpikesBend = true; else _randSpikesBend = false;
				_bendBottomLowerSpike = (_randSpikesBend) ? true : false;
				break;
			default:
				break;
			}
			
			int randSpikeEdgeSet1 = Random.Range(0,3);
			switch(randSpikeEdgeSet1){
			case 0:
				spikeEdgeSet1Sel = 0;
				break;
			case 1:
				spikeEdgeSet1Sel = 1;
				break;
			case 2:
				spikeEdgeSet1Sel = 2;
				int randSpikesBend = Random.Range(0,2);
				bool _randSpikesBend;
				if(randSpikesBend == 0) _randSpikesBend = true; else _randSpikesBend = false;
				_bendTopEdgeSet1 = (_randSpikesBend) ? true : false;
				break;
			default:
				break;
				
			}
			
			int randSpikeEdgeSet2 = Random.Range(0,3);
			switch(randSpikeEdgeSet2){
			case 0:
				spikeEdgeSet2Sel = 0;
				break;
			case 1:
				spikeEdgeSet2Sel = 1;
				break;
			case 2:
				spikeEdgeSet2Sel = 2;
				int randSpikesBend = Random.Range(0,2);
				bool _randSpikesBend;
				if(randSpikesBend == 0) _randSpikesBend = true; else _randSpikesBend = false;
				_bendTopEdgeSet2 = (_randSpikesBend) ? true : false;
				break;
			default:
				break;
			}
			
			int randSpikeDoubleSet1 = Random.Range(0,3);
			switch(randSpikeDoubleSet1){
			case 0:
				spikeDoubleSet1Sel = 0;
				break;
			case 1:
				spikeDoubleSet1Sel = 1;
				break;
			case 2:
				spikeDoubleSet1Sel = 2;
				break;
			default:
				break;
			}
			
			int randSpikeDoubleSet2 = Random.Range(0,3);
			switch(randSpikeDoubleSet2){
			case 0:
				spikeDoubleSet2Sel = 0;
				break;
			case 1:
				spikeDoubleSet2Sel = 1;
				break;
			case 2:
				spikeDoubleSet2Sel = 2;
				break;
			default:
				break;
			}
			
			int randSpikeCenterSet1 = Random.Range(0,3);
			switch(randSpikeCenterSet1){
			case 0:
				spikeCenterSet1Sel = 0;
				break;
			case 1:
				spikeCenterSet1Sel = 1;
				break;
			case 2:
				spikeCenterSet1Sel = 2;
				break;
			default:
				break;
			}
			
			int randSpikeCenterSet2 = Random.Range(0,3);
			switch(randSpikeCenterSet2){
			case 0:
				spikeCenterSet2Sel = 0;
				break;
			case 1:
				spikeCenterSet2Sel = 1;
				break;
			case 2:
				spikeCenterSet2Sel = 2;
				int randBigSpike = Random.Range(0,2);
				bool _randBigSpike;
				if(randBigSpike == 0) _randBigSpike = true; else _randBigSpike = false;
				_bigTopSpike = (_randBigSpike) ? true : false;
				break;
			default:
				break;
			}
	    }
	}
}
#endif