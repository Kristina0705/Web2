
import { Appeal } from "./AppealModel"

export interface AnswerModel {
	id: number
	appeal: Appeal,
	answertext: string,
    date:string,
    time:string,
}
export interface PostAnswer{
	appeal: Appeal|undefined,
	answertext: string,
}