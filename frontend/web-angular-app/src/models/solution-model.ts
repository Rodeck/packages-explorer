export interface Solution {
    uri: string;
    projects: Project[];
    stars: number;
    about: string;
    lastCommitDate: Date;
    commits: string;
    branches: number;
}

export interface Project {
    uri: string;
}