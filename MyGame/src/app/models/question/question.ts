import { Resource } from '../resource';

export class Question extends Resource  {
    description: string;
    yesNo: boolean;
    parentId: number;
    children: Array<Question>;
    showDescription: boolean = false;

}
