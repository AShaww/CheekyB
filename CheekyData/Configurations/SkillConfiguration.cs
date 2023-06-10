using CheekyModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace CheekyData.Configurations
{
    public class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.ToTable("Skill");
            builder.HasKey(s => s.SkillId);

            builder.Property(p => p.SkillId).ValueGeneratedOnAdd();
            builder.Property(p => p.SkillName).IsRequired();

            builder.HasOne(a => a.SkillType)
                   .WithMany(a => a.Skills)
                   .HasForeignKey(a => a.SkillTypeId);

            builder.ToTable("Skill").HasData(InitialRatingSeed());
        }

        private static IEnumerable<Skill> InitialRatingSeed()
        {
            return new List<Skill>
            {
                // Core Skills
                new() { SkillId = Guid.NewGuid(), SkillName = "C", SkillTypeId = 1 },
                new() { SkillId = Guid.NewGuid(), SkillName = "CSS", SkillTypeId = 1 },
                new() { SkillId = Guid.NewGuid(), SkillName = "HTML", SkillTypeId = 1 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Java", SkillTypeId = 1 },
                new() { SkillId = Guid.NewGuid(), SkillName = "JavaScript", SkillTypeId = 1 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Kotlin", SkillTypeId = 1 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Objective-C", SkillTypeId = 1 },
                new() { SkillId = Guid.NewGuid(), SkillName = "PHP", SkillTypeId = 1 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Python", SkillTypeId = 1 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Ruby", SkillTypeId = 1 },
                new() { SkillId = Guid.NewGuid(), SkillName = "SQL", SkillTypeId = 1 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Swift", SkillTypeId = 1 },
                new() { SkillId = Guid.NewGuid(), SkillName = "TypeScript", SkillTypeId = 1 },
                new() { SkillId = Guid.NewGuid(), SkillName = "XML", SkillTypeId = 1 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Net-Core-5", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Net-Framework", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Acceptance-tests", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Agile-Framework", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Amazon-CloudFront", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Amazon-Cognito", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Amazon-DynamoDB", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Amazon-EC2", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Amazon-ElastiCache", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Amazon-Elasticsearch-ES", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Amazon-EMR", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Amazon-Inspector", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Amazon-QuickSight", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Amazon-RDS", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Amazon-Route-53", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Amazon-S3", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Amazon-SageMaker", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Amazon-SNS", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Amazon-SQS", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Amazon-VPC", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Android", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Angular", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Ansible", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Apache-Hadoop-Mahout", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "ARM-Templates", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Aurelia", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Auth0", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Automation-Anywhere", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Automation-tests", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "AWS", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "AWS-Config", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "AWS-Control-Tower", SkillTypeId = 2 },
                new()
                {
                    SkillId = Guid.NewGuid(),
                    SkillName = "AWS-Developer-Tools-CodeBuild-CodeCommit-CodeDeploy-CodeStar", SkillTypeId = 2
                },
                new() { SkillId = Guid.NewGuid(), SkillName = "AWS-Direct-Connect", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "AWS-Elastic-Beanstalk", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "AWS-Fargate", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "AWS-Glue", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "AWS-Identity-Access-Management", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "AWS-Key-Management-Service-KMS", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "AWS-Lambda", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "AWS-Organizations", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "AWS-Service-Catalog", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "AWS-Systems-Manager", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "AWS-Transit-Gateway", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "AWS-WAF", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-Active-Directory", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-AD-B2C", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-API-Management-APIM", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-App-Insights", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-App-Services", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-Blob-Storage", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-Cognitive-Search", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-Content-Delivery-Network", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-Cosmos-DB", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-Data-Factory", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-Data-Lake", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-Databricks", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-Event-Grid", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-Event-Hubs", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-Front-Door", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-Functions", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-Key-Vault", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-Kubernetes-Service-AKS", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-Logic-Apps", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-Monitor", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-Notification-Hubs", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-Power-Automate", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-Service-Bus", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-SignalR", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-SQL-Database", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-Synapse-Analytics", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Azure-Table-Storage", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Bicep", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "BluePrism", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "CI-CD", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Code-review-process", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "DataIQ", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Dependency-injection", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Development-standards", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Docker", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Elastic-Stack", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "ETL", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Event-driven-architecture", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "GCP", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "GCP-AutoML", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "GCP-BigQuery", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "GCP-DataFlow", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "GCP-Dataform", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "GCP-DataStudio", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "GCP-Kubernetes-Engine", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "GDS-standards", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "GitHub-Actions", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "GitLab-Runners", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Infrastructure-as-Code-IaC", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Integration-tests", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "iOS", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Jenkins", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Jupyter", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Kafka", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Load-testing", SkillTypeId = 2 },
                new()
                {
                    SkillId = Guid.NewGuid(), SkillName = "Microsoft-SQL-Server-Analysis-Services-SSAS", SkillTypeId = 2
                },
                new()
                {
                    SkillId = Guid.NewGuid(), SkillName = "Microsoft-SQL-Server-Integration-Services-SSIS",
                    SkillTypeId = 2
                },
                new() { SkillId = Guid.NewGuid(), SkillName = "MongoDB", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "NodeJS", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "OAuth2-OIDC", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Object-oriented-programming", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Okta", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Performance-testing", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "PHP", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Python", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "PyTorch", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "QlikSense", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "React", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "React-Native", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Relational-databases", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Remix", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Ruby", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Scaled-Agile-Framework-SAFe", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Scrum", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Source-Control-Management-Git-SVN-Mercurial-TFS", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Spotfire", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "SQL", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Starburst-Galaxy", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Swift", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Tableau", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "TeamCity", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Tensorflow", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Typescript", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Unit-tests", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Vue-js", SkillTypeId = 2 },
                new() { SkillId = Guid.NewGuid(), SkillName = "Xamarin", SkillTypeId = 2 }
            };
        }
    }
}
