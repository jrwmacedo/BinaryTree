import { Resource } from '../resource';

export class Answer extends Resource  {
    /**
     * Initialize the answer class
     */
    constructor() {
        super();
        this.Answers = new Array<number>()
    }
    Answers: Array<number>;
}
