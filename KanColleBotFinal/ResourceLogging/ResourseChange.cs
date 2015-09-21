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
                return oldFuel - newFuel;
            }
        }
        public int ammoChange
        {
            get
            {
                return oldAmmo - newAmmo; 
            }
        }
        public int steelChange
        {
            get
            {
                return oldSteel - newSteel; 
            }
        }
         public int bauxiteChange
        {
            get
            {
                return oldBauxite - newBauxite; 
            }
        }
         public int instantConstructionChange
        {
            get
            {
                return oldInstantConstruction - newInstantConstruction; 
            }
        }
         public int instantRepairChange
        {
            get
            {
                return oldInstantRepair - newInstantRepair; 
            }
        }
        public int developmentMaterialChange
        {
            get
            {
                return oldDevelopmentMaterial - newDevelopmentMaterial; 
            }
        }
    }
}
