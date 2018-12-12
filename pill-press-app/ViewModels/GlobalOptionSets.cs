using System.ComponentModel.DataAnnotations;

namespace Gov.Jag.PillPressRegistry.Public.ViewModels
{

    public enum Contacttypes
    {
        [Display(Name = "Primary")]
        Primary = 931490000,
        [Display(Name = "Additional")]
        Additional = 931490001,
        [Display(Name = "BceID")]
        BceID = 931490002
    }


    public enum Equipmenttype
    {
        [Display(Name = "Pill Press")]
        PillPress = 931490000,
        [Display(Name = "Encapsulator")]
        Encapsulator = 931490001,
        [Display(Name = "Die, Mould or Punch")]
        DieMouldorPunch = 931490003,
        [Display(Name = "Pharmaceutical Mixer or Blender")]
        PharmaceuticalMixerorBlender = 931490004,
        [Display(Name = "Other")]
        Other = 931490005
    }


    public enum Levelofequipmentautomation
    {
        [Display(Name = "Semi-Automatic")]
        SemiAutomatic = 931490000,
        [Display(Name = "Automated")]
        Automated = 931490001,
        [Display(Name = "Capable of Being Automated")]
        CapableofBeingAutomated = 931490002,
        [Display(Name = "Manual")]
        Manual = 931490003
    }


    public enum Pillpressencapsulatorsize
    {
        [Display(Name = "Table Top Model")]
        TableTopModel = 931490000,
        [Display(Name = "Free Standing Model")]
        FreeStandingModel = 931490001,
        [Display(Name = "Industrial Model")]
        IndustrialModel = 931490003,
        [Display(Name = "Other")]
        Other = 931490002
    }


    public enum Howwasequipmentbuilt
    {
        [Display(Name = "Commercially Manufactured")]
        CommerciallyManufactured = 931490000,
        [Display(Name = "Custom-built")]
        Custombuilt = 931490001,
        [Display(Name = "Other")]
        Other = 931490002
    }


    public enum Productcategory
    {
        [Display(Name = "Consumable")]
        Consumable = 931490000,
        [Display(Name = "Non-Consumable")]
        NonConsumable = 931490001,
        [Display(Name = "Other")]
        Other = 931490002
    }


    public enum Productsubcategory
    {
        [Display(Name = "Food")]
        Food = 931490000,
        [Display(Name = "Health Product")]
        HealthProduct = 931490001,
        [Display(Name = "Candy")]
        Candy = 931490002,
        [Display(Name = "Batteries")]
        Batteries = 931490003,
        [Display(Name = "Cosmetics")]
        Cosmetics = 931490004,
        [Display(Name = "Other")]
        Other = 931490005
    }

}