using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyKnowledge.Authentication {
    // Business layer class must inherit from UserManager with
    // CustomUser and Guid as types
    //public class CustomUserManager : UserManager<CustomUser, Guid> {
    //    private readonly ICustomUserDocumentDBRepository repository;
    //    private readonly ICustomEmailService emailService;
    //    private readonly ICustomTokenProvider tokenProvider;

    //    // Parameters being injected by Unity.
    //    // container.RegisterType<ICustomUserMongoRepository, CustomUserMongoRepository>();
    //    // ..
    //    // ..
    //    public AuthorizationManager(
    //                 ICustomUserDocumentDBRepository repository,
    //                 ICustomEmailService emailService,
    //                 ICustomTokenProvider tokenProvider
    //                 ) 
    //                                 // calling base constructor passing
    //                                 // a repository which implements
    //                                 // IUserStore, among others.
    //                                 : base(repository)
    //      {
    //        this.repository = repository;

    //        // this.EmailService it's a property of UserManager and
    //        // it has to be set to send emails by your class
    //        this.EmailService = emailService;

    //        // this.UserTokenProvider it's a property of UserManager and
    //        // it has to be set to generate tokens for user password
    //        // recovery and confirmation tokens
    //        this.UserTokenProvider = tokenProvider;
    //    }
    //}
}
