using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slider : MonoBehaviour
{
   public FloatVariable FloatVariable;
   public Slider Slider;

   private void Start()
   {
      Slider.value = FloatVariable.Value;
   }

   public void ChangeSlide()
   {
      FloatVariable.Value = Slider.value;
   }
}
