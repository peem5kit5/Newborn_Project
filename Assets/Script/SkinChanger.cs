using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
public class SkinChanger : MonoBehaviour
{
    public static SkinChanger Instance { get; private set; }
    SkeletonAnimation skAnim;
    Skeleton skeleton;
    Spine.AnimationState animState;
    public Skin Base;
    public Skin Helmet;
    public Skin Armour;
    public Skin Legging;

    private void Awake()
    {
        SetUp();
    }

    void SetUp()
    {
        skAnim = GetComponent<SkeletonAnimation>();
        skeleton = skAnim.Skeleton;

        animState = skAnim.AnimationState;
    }
    public void SetBase(string _skinName)
    {
        if (Base.Name != _skinName)
        {
            var _mixMatchSkin = new Skin("Player");

            var _skin = new Skin("Base");
            _skin.AddSkin(skeleton.Data.FindSkin(_skinName));
            Base = _skin;
            _mixMatchSkin.AddSkin(skeleton.Data.FindSkin(_skinName));

            _mixMatchSkin.AddSkin(Helmet);
            _mixMatchSkin.AddSkin(Armour);
            _mixMatchSkin.AddSkin(Legging);

            skeleton.SetSkin(_mixMatchSkin);
            skeleton.SetSlotsToSetupPose();
            animState.Apply(skeleton);
        }

       
    }
  
    public void SetHelmet(string _skinName)
    {
        if (Helmet.Name != _skinName)
        {
            var _mixMatchSkin = new Skin("Player");
            _mixMatchSkin.AddSkin(Base);

            var _skin = new Skin("Helmet");
            _skin.AddSkin(skeleton.Data.FindSkin(_skinName));
            Helmet = _skin;
            _mixMatchSkin.AddSkin(skeleton.Data.FindSkin(_skinName));

            _mixMatchSkin.AddSkin(Armour);
            _mixMatchSkin.AddSkin(Legging);



          


            skeleton.SetSlotsToSetupPose();
            animState.Apply(skeleton);
        }
    }
    public void SetArmour(string _skinName)
    {
        if (Armour.Name != _skinName)
        {

            var _mixMatchSkin = new Skin("Player");
            _mixMatchSkin.AddSkin(Base);
            _mixMatchSkin.AddSkin(Helmet);

            var _skin = new Skin("Armour");
            _skin.AddSkin(skeleton.Data.FindSkin(_skinName));
            Armour = _skin;
            _mixMatchSkin.AddSkin(skeleton.Data.FindSkin(_skinName));

            _mixMatchSkin.AddSkin(Legging);


           

            skeleton.SetSlotsToSetupPose();
            animState.Apply(skeleton);
        }
    }
    public void SetLegging(string _skinName)
    {
        if (Legging.Name != _skinName)
        {
            var _mixMatchSkin = new Skin("Player");
            _mixMatchSkin.AddSkin(Base);
            _mixMatchSkin.AddSkin(Helmet);
            _mixMatchSkin.AddSkin(Armour);

            var _skin = new Skin("Legging");
            _skin.AddSkin(skeleton.Data.FindSkin(_skinName));
            Legging = _skin;
            _mixMatchSkin.AddSkin(skeleton.Data.FindSkin(_skinName));



            

            skeleton.SetSlotsToSetupPose();
            animState.Apply(skeleton);
        }
    }
   

}
