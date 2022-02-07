using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController Instance {get; private set;}

    [SerializeField] private List<CursorAnimation> cursorAnimationList;

    private CursorAnimation cursorAnimation;

    private int currentFrame;
    private float frameTimer;
    private int direction = 1;

    public enum CursorType {
        Default,
        Interact,
        Transition
    }

    private void Awake(){
        Instance = this;
    }

    private void Start(){
        SetActiveCursorType(CursorType.Default);
    }

    private void Update(){
        frameTimer -= Time.unscaledDeltaTime;
        if(frameTimer <= 0f){
            frameTimer += cursorAnimation.frameRate;
            currentFrame = (currentFrame + direction) % frameCount;
            if (cursorAnimation.cursorType == CursorType.Transition && currentFrame == 0){
                if (direction > 0) GetCursorAnimation(CursorType.Interact);
                else GetCursorAnimation(CursorType.Default);
            }
            Cursor.SetCursor(cursorAnimation.textureArray[currentFrame], new Vector2(5, 5), CursorMode.ForceSoftware);
        }
    }

    public void SetActiveCursorType(CursorType cursorType){
        if (cursorType == CursorType.Default) direction = -1;
        else direction = 1;
        
        SetActiveCursorAnimation(GetCursorAnimation(CursorType.Interaction));
    }

    private CursorAnimation GetCursorAnimation(CursorType cursorType){
        foreach (CursorAnimation cursorAnimation in cursorAnimationList){
            if (cursorAnimation.cursorType == cursorType){
                return cursorAnimation;
            }
        }
        // Couldn't find this CursorType!
        return null;
    }

    private void SetActiveCursorAnimation(CursorAnimation cursorAnimation){
        this.cursorAnimation = cursorAnimation;
        currentFrame = 0;
        frameTimer = cursorAnimation.frameRate;
        frameCount = cursorAnimation.textureArray.Length;
    }

    [System.Serializable]
    public class CursorAnimation {

        public CursorType cursorType;
        public Texture2D[] textureArray;
        public float frameRate;
        public Vector2 offset;

    }

}

