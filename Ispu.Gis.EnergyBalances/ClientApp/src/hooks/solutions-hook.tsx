import {useCallback, useState} from "react";
import {ISolution} from "../models/solution";
import {useSolutionService} from "./solution-service-hook";

export const useSolutions = () => {
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [solutions, setSolutions] = useState<ISolution[]>([]);
    const service = useSolutionService();

    const loadSolutions = useCallback(async () => {
        setIsLoading(true);
        await service.getAllSolutions().then(result => {
            setSolutions(result!);
        }).finally(() => setIsLoading(false))
    }, [service])
    return [isLoading, loadSolutions, solutions] as const;
}