import { createReducer, on } from "@ngrx/store";
import { initialAppliesState } from "./applies.state";
import { createApply, createApplyFailure, createApplySuccess, loadApplyList, loadApplyListFailure, loadApplyListSuccess } from "./applies.actions";


export const appliesReducer = createReducer(
    initialAppliesState,
    on(loadApplyList, (state) => {
        return {
            ...state,
            loading: true,
            error: null
        }
    }),
    on(loadApplyListSuccess, (state, action) => {
        return {
            ...state,
            applyList: [...action.applyList.applies],
            loading: false
        }
    }),
    on(loadApplyListFailure, (state, action) => {
        return {
            ...state,
            applyList: [],
            loading: false,
            error: action.errorObject
        }
    })
    // on(createApply, (state) => {
    //     return {
    //         ...state,
    //         loading: true,
    //         error: null
    //     }
    // }),
    // on(createApplySuccess, (state, action) => {
    //     return {
    //         ...state,
    //         isSuccess: true,
    //         loading: false
    //     }
    // }),
    // on(createApplyFailure, (state, action) => {
    //     return {
    //         ...state,
    //         isSuccess: false,
    //         loading: false,
    //         error: action.errorObject
    //     }
    // })
);