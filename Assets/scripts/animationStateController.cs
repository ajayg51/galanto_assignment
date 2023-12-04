using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class animationStateController : MonoBehaviour
{
    public Animator animator;
    public int isCurlBicepsHash, isRaiseArmsHash, isRotateArmsHash;
    public TMP_Text countText, levelText, exerciseName, infoText;
    public int animationStopCount = 0;
    public int countTextVal = 0;
    public int levelTextVal = 1;
    public bool isLevelZeroActivated = true;
    public bool isLevelOneActivated = false;
    public bool isLevelTwoActivated = false;
    bool isMobileScreenTouched = false;
    Touch touch ;
    
    
    // Start is called before the first frame update
    void Start()
    {   
        
        animator = GetComponent<Animator>();
        isCurlBicepsHash = Animator.StringToHash("isCurlBiceps");
        isRaiseArmsHash = Animator.StringToHash("isRaiseArms");
        isRotateArmsHash = Animator.StringToHash("isRotateArms");
        Debug.Log(animator);
    }

    // Update is called once per frame
    void Update()

    {   AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        isMobileScreenTouched = Input.touchCount > 0;
        if(isMobileScreenTouched){
            touch = Input.GetTouch(0);
        }

        if(stateInfo.normalizedTime >=1 && stateInfo.normalizedTime <2){
            onAnimationStopped();
        }else{
            if(isMobileScreenTouched){
                playOnMobile();
            }else{
                playOnEditor();
            }
        }
    }


    private void onAnimationStopped(){
        if(animator.GetBool(isCurlBicepsHash) == true){
                animationStopCount++;
                animator.SetBool(isCurlBicepsHash,false);
                if(animationStopCount==3){
                    levelTextVal = 2;
                    animationStopCount = 0;
                    isLevelZeroActivated = false;
                    isLevelOneActivated = true;
                    infoText.text = "Please press a on editor or tap on left half of mobile screen to begin";
                    exerciseName.text = "Exercise : Raise Arms";
                    levelText.text = "Level : "+levelTextVal.ToString();
                    countText.text = "Count : "+animationStopCount.ToString();
                } 
            }else if(animator.GetBool(isRaiseArmsHash) == true){
                animationStopCount++;
                animator.SetBool(isRaiseArmsHash,false);
                if(animationStopCount==3){
                    levelTextVal = 3;
                    animationStopCount = 0;
                    isLevelZeroActivated = false;
                    isLevelOneActivated = false;
                    isLevelTwoActivated = true;
                    infoText.text = "Please press r on editor or tap on left half of mobile screen to begin";
                    exerciseName.text = "Exercise : Rotate Arms";
                    levelText.text = "Level : "+levelTextVal.ToString();
                    countText.text = "Count : "+animationStopCount.ToString();
                }
            }else if(animator.GetBool(isRotateArmsHash) == true){
                animationStopCount++;
                animator.SetBool(isRotateArmsHash,false);
                if(animationStopCount==3){
                    levelTextVal = 1;
                    animationStopCount = 0;
                    isLevelZeroActivated = true;
                    isLevelOneActivated = false;
                    isLevelTwoActivated = false;
                    onKeySPressed();
                    infoText.text = "Please press b on editor or tap on left half of mobile screen to begin";
                    exerciseName.text = "All Levels finished, welldone!";
                    levelText.text = "Level : "+levelTextVal.ToString();
                    countText.text = "Count : "+animationStopCount.ToString();
                 }
            }
    }

    private void playOnMobile(){
        if(isMobileScreenTouched){
            if(touch.position.x < Screen.width/2){
                if(isLevelZeroActivated){
                    onKeyBPressed();
                    exerciseName.text = "Exercise : Curl Biceps";
                    countText.text = "Count : "+(animationStopCount+1).ToString();
                    infoText.text = "Please do this exercise 3 times, tap on left half of mobile screen to begin";
                }else if(isLevelOneActivated){
                    onKeyAPressed();
                    exerciseName.text = "Exercise : Raise arms";
                    countText.text = "Count : "+(animationStopCount+1).ToString();
                    infoText.text = "Please do this exercise 3 times, tap on left half of mobile screen to begin";
                }else if(isLevelTwoActivated){
                    onKeyRPressed();
                    exerciseName.text = "Exercise : Rotate arms";
                    countText.text = "Count : "+(animationStopCount+1).ToString();
                    infoText.text = "Please do this exercise 3 times, tap on left half of mobile screen to begin";
                }
            }else{
                onKeySPressed();
                infoText.text = "You have stopped the game, please tap on left half of mobile screen to begin";
            }
        }
    }

    private void playOnEditor(){
        if(infoText.text == ""){
            infoText.text = "Press b on editor or tap on left half of mobile screen to curl biceps, press s to stop the game at any stage";
        }
        if(Input.GetKey("b") && isLevelZeroActivated){
                onKeyBPressed();
                exerciseName.text = "Exercise : Curl Biceps";
                countText.text = "Count : "+(animationStopCount+1).ToString();
                infoText.text = "Please do this exercise 3 times, press b to start and s to stop the game";
            }else if(Input.GetKey("a") && isLevelOneActivated){
                onKeyAPressed();
                exerciseName.text = "Exercise : Raise arms";
                countText.text = "Count : "+(animationStopCount+1).ToString();
                infoText.text = "Please do this exercise 3 times, press a to start and s to stop the game";
            }else if(Input.GetKey("r") && isLevelTwoActivated){
                onKeyRPressed();
                exerciseName.text = "Exercise : Rotate arms";
                countText.text = "Count : "+(animationStopCount+1).ToString();
                infoText.text = "Please do this exercise 3 times, press r to start and s to stop the game";
            }else if(Input.GetKey("s")){
                if(isLevelZeroActivated){
                    infoText.text ="You have stopped the game, press b to curl biceps";
                }else if(isLevelOneActivated){
                    infoText.text ="You have stopped the game, press a to raise arms";
                }else if(isLevelTwoActivated){
                    infoText.text ="You have stopped the game, press r to rotate arms";
                }
                onKeySPressed();
            }
    }

    private void onKeyBPressed(){
        if(animator.GetBool(isRaiseArmsHash) == true || 
                animator.GetBool(isRotateArmsHash) == true ){
                animator.SetBool(isRaiseArmsHash,false);
                animator.SetBool(isRotateArmsHash,false);
        }

            animator.SetBool(isCurlBicepsHash,true);
    }

    private void onKeyAPressed(){
        if(animator.GetBool(isCurlBicepsHash) == true || 
            animator.GetBool(isRotateArmsHash) == true ){
            animator.SetBool(isCurlBicepsHash,false);
            animator.SetBool(isRotateArmsHash,false);
        }
            
        animator.SetBool(isRaiseArmsHash,true);
    }

    private void onKeyRPressed(){
        if(animator.GetBool(isCurlBicepsHash) == true || 
            animator.GetBool(isRaiseArmsHash) == true ){
            animator.SetBool(isCurlBicepsHash,false);
            animator.SetBool(isRaiseArmsHash,false);
        }

        animator.SetBool(isRotateArmsHash,true);
    }

    private void onKeySPressed(){
        if(animator.GetBool(isCurlBicepsHash) == true){
            animator.SetBool(isCurlBicepsHash,false);
        } else if(animator.GetBool(isRaiseArmsHash) == true){
            animator.SetBool(isRaiseArmsHash,false);
        } else if(animator.GetBool(isRotateArmsHash) == true){
            animator.SetBool(isRotateArmsHash,false);
        }
    }
}
