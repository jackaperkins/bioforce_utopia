using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingCanvas : MonoBehaviour {
    public Text scroller;
    float lifeTimer = 8;

    float timer;

    string sampleText = @"SYSCALL_DEFINE3(silly_copy,
                unsigned long *, src,
                unsigned long *, dst,
                unsigned long len)
{
    unsigned long buf;

    /* copy src, which is in the user’s address space, into buf */
    if (copy_from_user(&buf, src, len))
        return -EFAULT;

    /* copy buf into dst, which is in the user’s address space */
    if (copy_to_user(dst, &buf, len))
        return -EFAULT;

    /* return amount of data copied */
    return len;
Airi Satou  Accountant  Tokyo   33  2008/11/28  $162,700
Angelica Ramos  Chief Executive Officer (CEO)   London  47  2009/10/09  $1,200,000
Ashton Cox  Junior Technical Author     San Francisco   66  2009/01/12  $86,000
Bradley Greer   Software Engineer   London  41  2012/10/13  $132,000
Brenden Wagner  Software Engineer   San Francisco   28  2011/06/07  $206,850
Brielle Williamson  Integration Specialist  New York    61  2012/12/02  $372,000
Bruno Nash  Software Engineer   London  38  2011/05/03  $163,500
}";

	void Start () {
		
	}
	

	void Update () {
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0){
            Transition.instance.ChangeScene("new_basic");
        }

        timer -= Time.deltaTime;
        if (timer < 0) {
            string[] lines = sampleText.Split('\n');
            if (Random.value > 0.5f)
            {
                timer = Random.value * 0.1f;
                scroller.text += lines[(int)(Random.value * lines.Length)] + "\n";
            }
            else
            {
                timer = Random.Range(0.1f, 0.4f);
                scroller.text += Random.value.ToString() + " - INDEX \n";
                if(Random.value > 0.5f) {
                    scroller.text += "      - " + Random.value.ToString() + "// \n";
                }
            }
        }
	}
}
