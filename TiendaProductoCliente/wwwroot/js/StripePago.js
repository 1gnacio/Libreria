redirectToCheckout = function (sessionId) {
    var stripe = Stripe('pk_test_51KXZ1GJDmdm0VcVMfIpcCtdgbGGlQwwdIlKS7j1mmSNSDVyHP1bDnBJjgeWmHtJIkD8B9LxcmoEk3O54nwE61DBK00YG6cjsSy');
    stripe.redirectToCheckout({
        sessionId: sessionId
    });
}