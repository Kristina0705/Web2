import axios from 'axios';
import {Answer} from "../../models/RequestModel";
import {TypeAppeal} from "../../models/TypeAppealModel";

const API_URL = "http://localhost:8080/types-appeal/";


class TypeAppealService {
        getTypesAppeal(){
            return axios.get(API_URL + "get")
            .then((response) => {
                console.log(response.data);
                const data: Answer = response.data;
                if (data.status){
                  const types: TypeAppeal[] = data.answer.typesAppeal
                  return types;
                }
                return []
              })
              .catch((error) => {
                console.log(error);
                return []
              });
        }
}
export default new TypeAppealService();