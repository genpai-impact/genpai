using UnityEngine;

namespace Utils.LevelScene
{
    public class CameraMove : MonoBehaviour {
        private Camera c;
        private float mouseCenter;
        private int maxView = 120;
        private int minView = 10;
        private float slideSpeed = 20;

        private void Start() {
            //获取摄像头组件
            c = this.GetComponent<Camera>();
     
        }
 
 
        private void Update() {
         
            //获取虚拟按键(鼠标中轴滚轮)
            mouseCenter = Input.GetAxis("Mouse ScrollWheel");　　　　　　//鼠标滑动中键滚轮,实现摄像机的镜头放大和缩放          //mouseCenter < 0 = 负数 往后滑动,缩放镜头
            if (mouseCenter <0  ) {        　　　　　　　　　　　//滑动限制
                if (c.fieldOfView <= maxView) {
                    c.fieldOfView += 10 * slideSpeed*Time.deltaTime;
                    if (c.fieldOfView >= maxView) {
                        c.fieldOfView = minView;
                        
                    }
                                 
                }     
                //mouseCenter >0 = 正数 往前滑动,放大镜头
            } else if (mouseCenter >0 ) {　　　　　　　　　　　　//滑动限制
                if (c.fieldOfView >= minView) {
                    c.fieldOfView -= 10 * slideSpeed*Time.deltaTime;
                    if (c.fieldOfView <= minView) {
                        c.fieldOfView = maxView;
                    }
                                 
                } 
            }
             
        }        
    }
}