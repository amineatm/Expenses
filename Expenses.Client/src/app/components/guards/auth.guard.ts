import { inject } from "@angular/core"
import { Auth } from "../../services/auth"
import { Router } from "@angular/router";

export const authGuard = () => {
    const authService = inject(Auth);
    const router = inject(Router);

    if (authService.IsAuthenticated()) return true;

    router.navigate(['/login'])
    return false
}