using Microsoft.AspNetCore.Mvc;

namespace Instituto_Frigga_Backend.Controllers
{
    public class DocValidatorController : ControllerBase
    {
        /// <summary>
        /// Função que valida o CPF do Usuário
        /// </summary>
        /// <param name="cpfUsuario"></param>
        /// <returns></returns>
        public bool ValidaCpf(string cpfUsuario)
        {
            cpfUsuario = cpfUsuario.Replace(" ", "").Replace(".", "").Replace("-", "").Replace("/", "");
            bool resultado    = false;
            int[] v1 = {10, 9, 8, 7, 6, 5, 4, 3, 2};

            string cpfCalculo = "";
            int resto         = 0;
            int calculo       = 0;

            string digito_v1 = "";
            string digito_v2 = "";

            cpfCalculo = cpfUsuario.Substring(0 , 9);

            for(int i = 0; i <= 8; i++){
                
                calculo = calculo + int.Parse(cpfUsuario[i].ToString() )* v1[i];
            }
           
            resto = calculo % 11;
            calculo = 11 - resto;

            if(calculo > 9 ){
                 digito_v1 = "0";
                
            }else{
                digito_v1 = calculo.ToString();
            }
            if( digito_v1 == cpfUsuario[9].ToString() ){
                resultado = true;

            }

            int[] v2 = {11, 10, 9, 8, 7 , 6, 5 , 4, 3 , 2};
            resto = 0;
            
            cpfCalculo = cpfCalculo + calculo;

            calculo = 0;

            for(int i = 0; i <= 9; i++){
                
                calculo = calculo + int.Parse(cpfUsuario[i].ToString() )* v2[i];
            }

            resto = calculo % 11;
            calculo = 11 - resto;

            if(calculo > 9 ){
                 digito_v2 = "0";
                
            }else{
                digito_v2 = calculo.ToString();
            }
            if( digito_v2 == cpfUsuario[9].ToString() ){
                resultado = true;

            }

            return resultado;
        }

        /// <summary>
        /// Função que valida o CNPJ do Usuário
        /// </summary>
        /// <param name="cnpjUsuario"></param>
        /// <returns></returns>
        public bool ValidaCnpj(string cnpjUsuario){

            bool resultado = false;

            int[] v1 = {5 , 4 , 3 , 2 , 9 , 8 , 7 , 6 , 5 , 4 , 3 , 2};
            
            string cnpjCalculo = "";
            int resto = 0;
            int calculo = 0;

            string digito_v1 = "";
            string digito_v2 = "";

            cnpjUsuario = cnpjUsuario.Replace(" ", "");
            cnpjUsuario = cnpjUsuario.Replace(".", "");
            cnpjUsuario = cnpjUsuario.Replace("-", "");
            cnpjUsuario = cnpjUsuario.Replace("/", "");

            cnpjCalculo = cnpjUsuario.Substring(0 , 13);

            

            for(int i = 0; i <= 11; i++){

                calculo = calculo + int.Parse(cnpjUsuario[i].ToString() )* v1[i];

            }

            resto = calculo % 11;
            calculo = 11 - resto;
           
            if(resto < 2){

                digito_v1 = "0";

            }else{
                digito_v1 = calculo.ToString();
            }
            if(digito_v1 == cnpjUsuario[12].ToString() ){

                resultado = true;
            }

            int[] v2 = {6 , 5 , 4 , 3 , 2 , 9 , 8 , 7 , 6 , 5 , 4 , 3 , 2};

            cnpjCalculo = cnpjCalculo + calculo;

            calculo = 0;



            for(int i = 0; i <= 12; i++){

                calculo = calculo + int.Parse(cnpjUsuario[i].ToString() )* v2[i];
            }

            resto = calculo % 11;
            calculo = 11 - resto;
           
            if(resto < 2){

                digito_v2 = "0";

            }else{
                digito_v2 = calculo.ToString();
            }
            if(digito_v2 == cnpjUsuario[13].ToString() ){

                resultado = true;
            }



            return  resultado;
        }

    }
}