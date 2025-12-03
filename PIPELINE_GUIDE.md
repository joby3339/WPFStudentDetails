# Environment Configuration Guide

This document describes the environment setup and pipeline structure for the WPF Student Details application.

## Environment Structure

### 1. Development Environment
- **Branch**: `develop`, `feature/*`
- **Pipeline**: `.github/workflows/dev-pipeline.yml`
- **Purpose**: Rapid development and initial testing
- **Deployment**: Automatic on push to develop branch
- **Testing**: Unit tests, integration tests, basic security scans
- **Retention**: 7 days

### 2. QA Environment  
- **Branch**: `qa`, `release/*`
- **Pipeline**: `.github/workflows/qa-pipeline.yml`
- **Purpose**: Comprehensive testing and quality assurance
- **Deployment**: Manual promotion from development or automatic on push to qa branch
- **Testing**: Full test suite, performance tests, security tests, UI automation
- **Retention**: 30 days

### 3. Staging Environment
- **Branch**: `staging`
- **Pipeline**: Environment promotion workflow
- **Purpose**: Production-like testing and final validation
- **Deployment**: Manual promotion from QA
- **Testing**: Production-readiness tests, load testing
- **Retention**: 60 days

### 4. Production Environment
- **Branch**: `main`
- **Pipeline**: `.github/workflows/ci-cd.yml` (Production Pipeline)
- **Purpose**: Live application deployment
- **Deployment**: Manual promotion from staging or GitHub releases
- **Testing**: Smoke tests, monitoring validation
- **Retention**: Permanent

## Pipeline Workflows

### Development Pipeline (`dev-pipeline.yml`)
**Triggers:**
- Push to `develop` branch
- Push to `feature/*` branches  
- Pull requests to `develop`

**Jobs:**
1. **dev-build-and-test**
   - Build in Debug configuration
   - Run unit tests with code coverage
   - Code formatting and analysis
   - Upload development artifacts

2. **dev-integration-tests** 
   - Download development build
   - Run integration tests
   - Upload integration test results

3. **dev-security-scan**
   - Static security analysis
   - Dependency vulnerability scanning
   - Upload security reports

### QA Pipeline (`qa-pipeline.yml`)
**Triggers:**
- Push to `qa` branch
- Push to `release/*` branches
- Manual workflow dispatch

**Jobs:**
1. **qa-build**
   - Build in Release configuration with versioning
   - Comprehensive test execution
   - Generate build artifacts

2. **qa-functional-tests** (Matrix Strategy)
   - Smoke tests
   - Regression tests  
   - UI automation tests

3. **qa-performance-tests**
   - Memory usage testing
   - Load testing
   - Response time validation

4. **qa-security-tests**
   - Security vulnerability assessment
   - Dependency scanning
   - Static analysis

5. **qa-approval**
   - Results summary
   - Team notifications
   - QA sign-off process

### Production Pipeline (`ci-cd.yml`)
**Triggers:**
- Push to `main` branch
- Pull requests to `main`
- GitHub releases
- Manual deployment workflow

**Jobs:**
1. **production-build-and-test**
   - Production build validation
   - Final testing suite
   - Artifact generation

2. **create-release**
   - Multi-platform builds (win-x64, win-x86)
   - Release package creation
   - Automated release asset upload

### Environment Promotion (`environment-promotion.yml`)
**Triggers:**
- Manual workflow dispatch only

**Features:**
- Controlled environment promotion
- Validation of promotion paths
- Environment-specific testing
- Rollback capabilities
- Team notifications

## Branch Strategy

```
main (production)
├── staging  
│   ├── qa
│   │   ├── develop
│   │   ├── feature/user-management
│   │   ├── feature/data-validation
│   │   └── feature/ui-improvements
│   └── release/v1.1.0
└── hotfix/critical-bug-fix
```

### Branch Flow Rules

1. **Feature Development**: `feature/*` → `develop`
2. **QA Testing**: `develop` → `qa` 
3. **Staging Validation**: `qa` → `staging`
4. **Production Release**: `staging` → `main`
5. **Emergency Fixes**: `main` → `hotfix/*` → `main`

## Environment Protection Rules

### Development
- No protection rules
- Automatic deployments
- All developers have access

### QA  
- Require pull request reviews
- Require status checks to pass
- QA team approval required

### Staging
- Require pull request reviews  
- Require QA sign-off
- DevOps team approval required

### Production
- Require pull request reviews
- Require staging validation
- Multiple approvals required
- Deployment windows enforced

## Testing Strategy by Environment

### Development Testing
- Unit tests (required)
- Basic integration tests
- Code coverage reporting
- Security scanning

### QA Testing  
- Full unit test suite
- Integration tests
- UI automation tests
- Performance testing
- Security vulnerability assessment
- Manual testing validation

### Staging Testing
- Production readiness tests
- Load testing
- End-to-end testing
- Performance benchmarking
- Security penetration testing

### Production Testing
- Smoke tests
- Health checks
- Monitoring validation
- Rollback verification

## Artifact Management

### Naming Convention
- Development: `dev-build-{sha}`
- QA: `qa-build-{version}-{date}`
- Staging: `staging-build-{version}`
- Production: `prod-release-{version}`

### Retention Policy
- Development: 7 days
- QA: 30 days  
- Staging: 60 days
- Production: Permanent

## Notifications

### Slack Channels
- `#development` - Dev pipeline notifications
- `#qa-team` - QA pipeline results
- `#deployments` - Environment promotions
- `#alerts` - Pipeline failures and emergencies

### Email Notifications
- QA team for test failures
- DevOps team for deployment issues
- Management for production deployments

## Secrets and Environment Variables

### Required Secrets
- `DEV_SLACK_WEBHOOK` - Development notifications
- `QA_SLACK_WEBHOOK` - QA notifications  
- `DEPLOYMENT_SLACK_WEBHOOK` - Deployment notifications
- `GITHUB_TOKEN` - Repository access (auto-provided)

### Environment Variables
- `DOTNET_VERSION` - .NET SDK version
- `PROJECT_PATH` - Path to main project file
- `ENVIRONMENT` - Current environment name

## Manual Operations

### Promoting Between Environments
1. Go to Actions tab in GitHub
2. Select "Environment Promotion" workflow
3. Click "Run workflow"
4. Select source and target environments
5. Specify build artifact to promote
6. Execute promotion

### Emergency Procedures
1. **Rollback**: Use environment promotion to previous stable build
2. **Hotfix**: Create hotfix branch from main, deploy directly
3. **Pipeline Disable**: Disable workflows in repository settings

## Monitoring and Observability

### Pipeline Monitoring
- GitHub Actions dashboard
- Build success/failure rates
- Test result trends
- Deployment frequency

### Application Monitoring  
- Health check endpoints
- Performance metrics
- Error rate monitoring
- User activity tracking