import axios from 'axios';
import {Answer} from "../../models/RequestModel";
import authHeader from '../AuthHeader';
import {Appeal, PostAppeal} from "../../models/AppealModel";
import { TypeAppeal } from '../../models/TypeAppealModel';
import { AnswerModel,PostAnswer } from '../../models/AnswerModel';
import { Dates } from '../../models/DatesModel';

const API_URL = "http://localhost:8080/answers/";


class AnswerService {
        getAnswers(){
            return axios.get(API_URL + "get-all-answers",{headers:authHeader()})
            .then((response) => {
                console.log(response.data);
                const data: Answer = response.data;
                if (data.status){
                  const answers: AnswerModel[] = data.answer.answers
                  return answers;
                }
                return []
              })
              .catch((error) => {
                console.log(error);
                return []
              });
        }
        
        AddAnswer(data:PostAnswer){
            return axios.post(API_URL + "add-answer",data,{headers:authHeader()})
            .then((response) => {
                const data: Answer = response.data;
                  return data.status;
              })
              .catch((error) => {
                console.log(error);
                return false;
              });
        }
        MakeReport(data:Dates){
            return axios.post(API_URL + "report",data,{headers:authHeader()})
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
export default new AnswerService();