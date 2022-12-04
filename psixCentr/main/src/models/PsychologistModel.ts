import { User } from "./UserModel";
import { AnswerModel } from "./AnswerModel";

export interface Psychologist {
	id: number
    user:User,
	name: string,
	surname: string,
	middlename:string,
    answerlist:AnswerModel[],
}