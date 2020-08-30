using SQLite;
using SQLiteNetExtensions.Attributes;

namespace SmartWMS.Models
{
    [Table("Items")]

    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int ItemId { get; set; }
        public string ItemBarcode { get; set; }
        public string ItemName { get; set; }

        /*
        public override string ToString()
        {
            return ItemId + " " + ItemBarcode + " " + ItemName;
        }
        */
    }
}


//868138123742	SWT,ZARS                                
//868138123759	SWT,ZARS                                
//868138123793	SWT,ZARS                                
//868138123809	SWT,ZARS                                
//868138123773	SWT,ZARS                                
//868138123729	SWT,ZARS                                
//868138123712	SWT,ZARS                                
//868138123743	SWT,ZARS                                
//868138123750	SWT,ZARS                                
//868138123728	ŞRT,FILLO-0S                            
//299997123396	PNT,ARS / CRP-NAVY                      
//299997123433	PNT,ARS / FGP-BEIGE                     
//299997123457	PNT,ARS / JTK-ANTHRACITE                
//299997123518	SWT,FORGAN / CVP-GREEN                  
//299997123833	UK.BDY,MASLAK / Q0G-NEON GREEN
//299997123600	UK.ELB,KANSU / D6Z-DARK YELLOW
//299997123697	TYT,BRONZ-A-20S / CT3-GREY MELANGE
//299997123703	TYT,BRONZ-A-20S / FTG-PINK              
//299997123633	UK.TSH,BS.STARWA / JYX-BRILLIANT WH
//299997123626	UK.TSH,BS.STARWA / JYX-BRILLIANT WH
//868138123132	UK.TSH,BS.STARWARS-3                    
//868138123034	PNT,SLIM-MOM-MID-L-YD                   
//868138123027	PNT,SLIM-MOM-MID-L-YD                   
//868138123065	PNT,SLIM-MOM-MID-L-YD                   
//868138123058	PNT,SLIM-MOM-MID-L-YD                   
//299996123735	PNT,MOM-ANTRACID / GRD-GREY RODEO
//299996123161	UK.ELB,BUZY-0W / MHS-BLACK JACQUA
//299996123154	UK.ELB,BUZY-0W / MHH-ANTHRACITE J
//299996123185	UK.ELB,BUZY-0W / MHS-BLACK JACQUA
//868138123965	UK.ELB,BUZY-0W                          
//868138123972	UK.ELB,BUZY-0W                          
//258805123571	TERE,ROSES-YD                           
//258805123588	TERE,ROSES-YD                           
//258805123595	TERE,ROSES-YD                           
//258805123601	TERE,SASHA-YD                           
//258805123618	TERE,SASHA-YD                           
//258805123663	KBN,CIBUTI-R                            
//258805123670	KBN,CIBUTI-R                            
//258805123687	KBN,CIBUTI-R                            
//258805123694	KBN,CIBUTI-R                            
//258805123700	KBN,CIBUTI-R                            
//258805123717	KBN,CIBUTI-R                            
//868138123600	KK.TSH,DUBAR                            
//868138123617	KK.TSH,DUBAR                            
//868138123049	KK.BDY,WEFSE                            
//868138123056	KK.BDY,WEFSE                            
//868138123087	KK.BDY,WEFSE                            
//868138123946	KK.TSH,DUBAR                            
//868138123953	KK.TSH,DUBAR                            
//299997123726	KK.TSH,B,Y.LIVE- / NOO-DEEP NAVY        