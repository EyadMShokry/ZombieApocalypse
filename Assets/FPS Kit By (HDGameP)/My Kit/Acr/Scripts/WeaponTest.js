var drawAnim : String = "Draw";
var fireLeftAnim : String = "Fire";
var reloadAnim : String = "Reload";
var animationGO : GameObject;
 
private var drawWeapon : boolean = false;
private var reloading : boolean = false;
 
function Start (){
DrawWeapon();
}
 
function Update (){
 
    if(Input.GetButtonDown ("Fire1") && reloading == false && drawWeapon == false){
        Fire();
        }
       
        if (Input.GetKeyDown ("r") && reloading == false && drawWeapon == false){
    Reloading();
        }
       
        if (Input.GetKeyDown ("1") && reloading == false){
        DrawWeapon();
        }      
}
 
function Fire(){
    animationGO.GetComponent.<Animation>().CrossFadeQueued(fireLeftAnim, 0.08, QueueMode.PlayNow);
}
 
function DrawWeapon() {
  if(drawWeapon)
    return;
       
        animationGO.GetComponent.<Animation>().Play(drawAnim);
        drawWeapon = true;
        yield WaitForSeconds(0.6);
        drawWeapon = false;
       
}
 
function Reloading(){
     if(reloading) return;
   
        animationGO.GetComponent.<Animation>().Play(reloadAnim);
        reloading = true;
        yield WaitForSeconds(2.0);
        reloading = false;
}