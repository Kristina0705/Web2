import axios from 'axios';
import {Answer} from "../../models/RequestModel";
import authHeader from '../AuthHeader';
import {Appeal, PostAppeal} from "../../models/AppealModel";
import { TypeAppeal } from '../../models/TypeAppealModel';

const API_URL = "http://localhost:8080/appeal/";


class AppealService {
        getAppeals(){
            return axios.get(API_URL + "get-all-appeals",{headers:authHeader()})
            .then((response) => {
                console.log(response.data);
                const data: Answer = response.data;
                if (data.status){
                  const appeals: Appeal[] = data.answer.appeals
                  return appeals;
                }
                return []
              })
              .catch((error) => {
                console.log(error);
                return []
              });
        }
        getHistoryAppeal(){
            return axios.get(API_URL + "history-appeal",{headers:authHeader()})
            .then((response) => {
                console.log(response.data);
                const data: Answer = response.data;
                if (data.status){
                  const appeals: Appeal[] = data.answer.appeals
                  const answers: Answer[] = data.answer.answers
                  const types: TypeAppeal[] = data.answer.typesAppeal
                  return {appeals:appeals,answers:answers,types:types} 
                }
                return {appeals:[],answers:[],types:[]}
              })
              .catch((error) => {
                console.log(error);
                return {appeals:[],answers:[],types:[]}
              });
        }
        filterAppeal(type:string){
            return axios.get(API_URL + "history-appeal?type="+type,{headers:authHeader()})
            .then((response) => {
                console.log(response.data);
                const data: Answer = response.data;
                if (data.status){
                  const appeals: Appeal[] = data.answer.appeals
                  const answers: Answer[] = data.answer.answers
                  const types: TypeAppeal[] = data.answer.typesAppeal
                  return {appeals:appeals,answers:answers,types:types} 
                }
                return {appeals:[],answers:[],types:[]}
              })
              .catch((error) => {
                console.log(error);
                return {appeals:[],answers:[],types:[]}
              });
        }
        AddAppeal(data:PostAppeal){
            return axios.post(API_URL + "add-appeal",data,{headers:authHeader()})
            .then((response) => {
                const data: Answer = response.data;
                  return data.status;
              })
              .catch((error) => {
                console.log(error);
                return false;
              });
        }
}
export default new AppealService();