using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SimioAPI;
using SimioAPI.Extensions;
using SimioAPI.Graphics;
using Simio;
using Simio.SimioEnums;
using QlmLicenseLib;
using System.Drawing.Printing;

namespace _MSY1_HT6_201314474
{   /*
        UNIVERSIDAD DE SAN CARLOS DE GUATEMALA
        FACULTAD DE INGENIERIA
        MODELACIÓN Y SIMULACION 1
        BAYRON ROMEO AXPUAC YOC
        201314474
        HOJA DE TRABAJO No. 6

        Esta hoja de trabajo consiste en plasmar por medio de la API de Simio
        El primer nombre, apellido y carnet del estudiante.

        Cabe mencionar que esta hoja de trabajo se desarrollo con algunos metodos
        creados y que utilizamos con mi pareja (P13) para el desarrollo de la práctica
        número 3 del laboratorio en el cual se solicito dicha hoja de trabajo.

        Tambien se adjunta un enlace de GitHub en el cual se puede descargar toda la
        solución de esta hoja de trabajo por si fuese necesario.
     
     */
    class Objetos
    {
        private ISimioProject proyectoApi;
        //Debemos tener en nuestra carpeta DEBUG del proyecto un archivo de simio con los
        //elementos necesario para la consturcción de este proyecto.
        private string rutabase = "[MYS1]ModeloBase_HT6_201314474.spfx";
        //Este será el nombre del archivo que se creará al momento de ejecutuar nuestro codgo.
        private string rutafinal = "[MYS1]ModeloFinal_HT6_2013144744.spfx";
        private string[] warnings;
        private IModel model;
        private IIntelligentObjects intelligentObjects;
        public Objetos()
        {
            //creamos el constructor de la clase en el cual se creara el proyecto y 
            // vamos a utilizar el modelo base para poder crear los elementos necesarios
            // para la consturccion de los datos correspondientes.
            proyectoApi = SimioProjectFactory.LoadProject(rutabase, out warnings);
            model = proyectoApi.Models[1];
            intelligentObjects = model.Facility.IntelligentObjects;
        }
        public void crear_carnet()
        {   
             //PRIMERO VAMOS A CREAR UN ARREGLO, ALGO EXTENSO, PARA PODER DEFINIR LAS COORDENADAS
             //DE CADA TRANSFERNODE QUE INDICARA LOS VERTICES O LOS PUNTOS EN LOS CUALES SE DEBEN
             //UNIR DICHOS ELEMENTOS PARA PODER FORMAR LOS NÚMEROS O LETRAS CORRESPONDIENTES.
            int[,] arrayCARNET = new int[,] {   
                                                //LAS PRIMERAS COORDINADAS INDICAN LAS POCIONES EN LOS CUALES
                                                //SE COLOCARAN LOS TRANSFERNODES PARA FORMAR EL CARNET 
                                                //DEL ESTUDIANTES 201314474, AL FINAL DE CADA LINEA SE MUESTRA
                                                //EL DIGITO CORRESPONDIENTE PARA IR FORMANDO EL NUMERO DE
                                                //CARNET DEL ESTUDIANTE.
                                                { 00,00},{10,00}, {10,10}, {0,10}, {0,20},{10,20}, //2
                                                { 20,00},{30,00}, {30,20}, {20,20},  //0
                                                { 40,00},{40,20},  //1
                                                { 50,00},{60,00}, {50,10}, {60,10}, {50,20},{60,20}, //3
                                                {70,00},{70,20},//1
                                                {80,00},{80,10},{90,10},{90,20},{90,00},//4
                                                {100,00},{100,10},{110,10},{110,20},{110,00},//4
                                                {120,00},{130,00},{130,10},{130,20},{120,10},//7
                                                {140,00},{140,10},{150,10},{150,20},{150,00},//4
                                                //LAS SIGUIANTES COORDINADAS INDICAN LAS POCIONES EN LOS CUALES
                                                //SE COLOCARAN LOS TRANSFERNODES PARA FORMAR EL PRIMER NOMBRE 
                                                //DEL ESTUDIANTES BAYRON ROMEO AXPUAC YOC, AL FINAL DE CADA LINEA 
                                                //SE MUESTRA LA LETRA CORRESPONDIENTE PARA IR FORMANDO EL PRIMER 
                                                //NOMBRE DEL ESTUDIANTE.
                                                {00,30},{10,30}, {10,40}, {10,50}, {0,50},{0,40}, //B
                                                {20,50},{20,40}, {20,30}, {30,30}, {30,40},{30,50}, //A
                                                {40,30},{40,40}, {45,40}, {50,40}, {50,30},{45,50}, //Y
                                                {60,50},{60,40}, {60,30}, {70,30}, {70,40},{80,50}, //R
                                                {90,30},{100,30}, {100,50}, {90,50},  //O
                                                {110,50},{110,30}, {120,50}, {120,30},  //N
                                                 //LAS SIGUIANTES COORDINADAS INDICAN LAS POCIONES EN LOS CUALES
                                                //SE COLOCARAN LOS TRANSFERNODES PARA FORMAR EL PRIMER NOMBRE 
                                                //DEL ESTUDIANTES BAYRON ROMEO AXPUAC YOC, AL FINAL DE CADA LINEA 
                                                //SE MUESTRA LA LETRA CORRESPONDIENTE PARA IR FORMANDO EL PRIMER 
                                                //NOMBRE DEL ESTUDIANTE.
                                                {00,80},{00,70}, {00,60}, {10,60}, {10,70}, {10,80}, //A
                                                {20,60}, {30,80}, {30,60}, {20,80},  //X
                                                {40,80},{40,70}, {40,60}, {50,60}, {50,70}, //P
                                                {60,60}, {60,80}, {70,80}, {70,60},  //U
                                                {80,80},{80,70}, {80,60}, {90,60}, {90,70}, {90,80}, //A
                                                {110,80}, {100,80}, {100,60}, {110,60}  //C
                                            };
            //Creación de transfernodes y enlaces
            for (int i = 0; i < arrayCARNET.GetLength(0); i++)
            {
                //CREAMOS UN TRANSFERNODE POR MEDIO DEL METODO CORRESPONDIENTE.
                createTransferNode("TN" + i, (arrayCARNET[i, 0]), (arrayCARNET[i, 1]));
                // SE FUE VERICANDO EL NOMBRE DE CADA UNO DE LOS TRANSFERNODE CREADOS Y SE FUE RECCORIENDO EL ARREGLO 
                // DE POSICIONES, Y SEGÚN LA ESTRUCUTRA DE CADA UNO DE LOS NUMEROS O LESTRAS CREADAS SE ELABORO UNA
                //UNION ENTRE ESTOS ELEMENTOS POR MEDIO DE UN PATH, ESTO SE REALIZO GRACIAS AL METODO CREATEPATH, 
                //QUE RECIBE COMO PARAMETROS DOS TRANSFERNODE Y GRACIAS A ESTE METODO SE PUEDEN IR CONECTANDO LOS
                //ELEMENTOS NECESARIOS.
                if ((i > 0 && i <= 5) || (i > 6 && i <= 9) || (i > 6 && i <= 9) || (i > 20 && i <= 23) || (i > 25 && i <= 28) || (i > 30 && i <= 33) || (i > 35 && i <= 38)
                    || (i > 40 && i <= 45) || (i > 46 && i <= 51) || (i > 52 && i <= 56) || (i > 58 && i <= 63) || (i > 64 && i <= 67) || (i > 68 && i <= 71)
                    || (i > 72 && i <= 77) || (i > 82 && i <= 86) || (i > 87 && i <= 90) || (i > 91 && i <= 96) || (i > 97 && i <= 100))
                {
                    createPath(get_nodo("TN" + (i - 1)), get_nodo("TN" + i));
                    if (i == 45) {
                        createPath(get_nodo("TN" + (i)), get_nodo("TN" + (i-5)));
                        createPath(get_nodo("TN" + (i)), get_nodo("TN" + (i - 3)));
                    }
                    else if (i == 50 ||  i == 62 || i== 76 || i == 86 || i == 95)
                    {
                        createPath(get_nodo("TN" + (i)), get_nodo("TN" + (i - 3)));
                    }
                }
                else if ( i == 57)
                {
                    createPath(get_nodo("TN" + (i)), get_nodo("TN" + (i - 3)));
                }
                else if (i == 24 || i == 29 || i == 34 || i == 39)
                {
                    createPath(get_nodo("TN" + (i)), get_nodo("TN" + (i - 2)));
                }
                else if (i == 10 || i == 68)
                {
                    createPath(get_nodo("TN" + (i - 1)), get_nodo("TN" + (i - 4)));
                }
                else if (i == 11 || i == 13 || i == 15 || i == 17 || i == 19 || i== 79 || i== 81)
                {
                    createPath(get_nodo("TN" + (i - 1)), get_nodo("TN" + i));
                    if (i == 15 || i == 17) {
                        createPath(get_nodo("TN" + (i)), get_nodo("TN" + (i - 2)));
                    }
                }               
            } 
        }  
        //Este metodo nos permite obtener un elemento (Transfernode) del modelo.
        public INodeObject get_nodo(String name)
        {
            return (INodeObject)model.Facility.IntelligentObjects[name];
        }
        //ESte metodo nos permite ejecutar el el metodo crear_carnet(), y de esta forma "imprimir" nuestros transfernodes
        //y sus uniones en el modelo final.
        public void crear_modelo()
        {
            crear_carnet();
        }
        //Este metodo nos permite crear un transfernode en el modelo final, dicho objeto se le asigna un nombre
        //y las coordenadas correspondientes donde sera "impreso" o colocado.
        public void createTransferNode(string nombre, int x, int y)
        {
            intelligentObjects.CreateObject("TransferNode", new FacilityLocation(x, 0, y));
            model.Facility.IntelligentObjects["TransferNode1"].ObjectName = nombre;
        }
        //ESte metodo recibe dos transfernodes previametne creados y los une por medio de un path.
        public void createPath(INodeObject nodo1, INodeObject nodo2)
        {
            this.createLink("Path", nodo1, nodo2);
        }
        //Este metodo permitirá crear un tipo de enlace entre dos elementos, para esta hoja de trabajo
        //se necesitaba elmentos paths que unieran dos transfernodes(nodo1 y nodo2) para poder unir y graficar
        //cada número o letra necesaria.
        public void createLink(String type, INodeObject nodo1, INodeObject nodo2)
        {
            intelligentObjects.CreateLink(type, nodo1, nodo2, null);
        }
        //Este es el metodo más imporatante ya que este ejecuta la creación del modelo final.y este es el metodo
        //que se debe de llamar desde el formulario.
        public void crearModelo()
        {
            crear_modelo();
            SimioProjectFactory.SaveProject(proyectoApi, rutafinal, out warnings);
        }
    }
}
