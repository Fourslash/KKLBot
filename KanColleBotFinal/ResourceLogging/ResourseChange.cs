using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanColleBotFinal.ResourceLogging
{
    public class ResourseChange
    {
        public int oldFuel;
        public int oldAmmo;
        public int oldSteel;
        public int oldBauxite;

        public int oldInstantConstruction;
        public int oldInstantRepair;
        public int oldDevelopmentMaterial;

        public int newFuel;
        public int newAmmo;
        public int newSteel;
        public int newBauxite;

        public int newInstantConstruction;
        public int newInstantRepair;
        public int newDevelopmentMaterial;

        public int fuelChange
        {
            get
            {
                return newFuel - oldFuel;
            }
        }
        public int ammoChange
        {
            get
            {
                return newAmmo - oldAmmo; 
            }
        }
        public int steelChange
        {
            get
            {
                return newSteel - oldSteel; 
            }
        }
         public int bauxiteChange
        {
            get
            {
                return newBauxite - oldBauxite; 
            }
        }
         public int instantConstructionChange
        {
            get
            {
                return newInstantConstruction - oldInstantConstruction; 
            }
        }
         public int instantRepairChange
        {
            get
            {
                return newInstantRepair - oldInstantRepair; 
            }
        }
        public int developmentMaterialChange
        {
            get
            {
                return newDevelopmentMaterial - oldDevelopmentMaterial; 
            }
        }
    }
}
