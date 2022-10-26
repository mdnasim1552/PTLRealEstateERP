<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EmployeeListEpic.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.EmployeeListEpic" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <style>
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" >

        .mt20 {
            margin-top: 20px;
        }

        .chzn-container {
            width: 100% !important;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 35px !important;
            line-height: 35px !important;
        }

        .card-body {
            min-height: 400px !important;
        }

        .pd4 {
            padding: 4px !important;
        }
    </style>
    

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>


            <div class="card mt-5">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Company
                                </label>
                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Department
                                </label>
                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:Label ID="lblDeptDesc" CssClass="dataLblview" runat="server" Visible="False"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-3" id="divSection" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblDept" CssClass="mb-2 d-block" runat="server">Section</asp:Label>


                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label1" CssClass="mb-2 d-block" runat="server">Page Size</asp:Label>


                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem Selected="True">300</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>



                        <div class="col-md-2 mt-4">
                          

                                <asp:CheckBox ID="chkbdate" runat="server" AutoPostBack="True" Visible="false" Font-Bold="True"/>
                                <asp:Label ID="withBirth" runat="server" cssclass="d-none">With Birth Date</asp:Label>
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-info btn-md mb-2" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>




                    </div>




                    <div runat="server">
                        <div class="row ml-4">
                            <div class="col-md-2" id="desFrom" runat="server" style="display:none;">
                                <div class="form-group">
                                    <asp:Label ID="lblfrmd" CssClass="mb-2 d-block" runat="server">From</asp:Label>

                                    <asp:DropDownList ID="ddlfrmDesig" runat="server" OnSelectedIndexChanged="ddlfrmDesig_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control form-control-sm" TabIndex="6">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-2" id="desTo" runat="server" style="display:none;">
                                <div class="form-group">
                                    <asp:Label ID="lbltdeg" CssClass="mb-2 d-block" runat="server">To</asp:Label>

                                    <asp:DropDownList ID="ddlToDesig" runat="server" CssClass="form-control form-control-sm" TabIndex="6">
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>
                    </div>

                    <div class="row">


                        <%--<div class="col-md-4" id="SepType" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Label ID="Label6" CssClass="mb-2 d-block" runat="server">Separeation Type</asp:Label>

                                <asp:DropDownList ID="ddlSepType" runat="server" Width="233" CssClass="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlToDesig_SelectedIndexChanged" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>--%>

                        <div class="col-md-3" runat="server" id="comlist" visible="False">
                            <div class="form-group">
                                <asp:Label CssClass="mb-2 d-block" runat="server">Companies</asp:Label>
                                <asp:DropDownList ID="ddlComName" class="ComName form-control ClCompAndMod" runat="server" TabIndex="2" Width="224">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card card-fluid">
                    <div class="card-body" style="height:100PX; overflow-x: scroll; overflow: scroll;">
                        <div class="row">
                            <asp:GridView ID="gvEmpList" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            AutoGenerateColumns="False" OnPageIndexChanging="gvEmpList_PageIndexChanging" AllowPaging="true"
                                            ShowFooter="True" PageSize="10">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Company Name">

                                                    <HeaderTemplate>
                                                        <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                            Text="Company Name" Width="140px"></asp:Label>
                                                        <asp:HyperLink ID="hlbtntbCdataExcelemplist" runat="server"
                                                            CssClass="btn  btn-success btn-sm" ToolTip="Export Excel"><i class="fa fa-file-excel"></i></asp:HyperLink>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvcustname" runat="server"
                                                            Text='<%# "<B>" +Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname"))+"<B>" %>'
                                                            Width="180px"></asp:Label>
                                                    </ItemTemplate>



                                                    <ItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                </asp:TemplateField>
                                           

                                                <asp:TemplateField HeaderText="Department Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvdepname" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Employee Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvdeptandemployeeemp" runat="server"
                                                            Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                              Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")).Trim()+". "+
                                                                             Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "")  %>'
                                                            Width="150px"> 
                                              
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Image">
                                                    <ItemTemplate>
                                                        <%--<asp:Image ID="userimg" runat="server" style="width:50px;" ImageUrl="~/image/profile_img.png" />--%> 
                                                        <asp:Image ID="userimg" runat="server" style="width:50px;" ImageUrl="~/Upload/UserImages/3101001.png" />
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Father's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvfname" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fname")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Mother's Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvmname" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mname")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcardnoemp" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvdesignationemp" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                            Width="120px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Higher Education ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvheducation" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "heducation")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Joining Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvjoindateemp" runat="server" BackColor="Transparent"
                                                            BorderStyle="None"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Confirm Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvconfirmdate" runat="server" BackColor="Transparent"
                                                            BorderStyle="None"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "confirmdate")).ToString("dd-MMM-yyyy") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Blood Group">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvbloodgrp" runat="server" BackColor="Transparent"
                                                            BorderStyle="None"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "blood")) %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Mobile">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvmobile" runat="server" BackColor="Transparent"
                                                            BorderStyle="None"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobileno")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Email">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmail" runat="server" BackColor="Transparent"
                                                            BorderStyle="None"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="National Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvnationalid" runat="server" BackColor="Transparent"
                                                            BorderStyle="None"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nationalidcard")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Extention">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblExtion" runat="server" BackColor="Transparent"
                                                            BorderStyle="None"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "extention")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <%--<FooterTemplate>
                                                        <asp:Label ID="lgvTotalnagad" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: left" Text="Total"></asp:Label>
                                                    </FooterTemplate>--%>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Religion">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvreligion" runat="server" BackColor="Transparent"
                                                            BorderStyle="None"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "religion")) %>'
                                                            Width="45px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Sex">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvSex" runat="server" BackColor="Transparent"
                                                            BorderStyle="None"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sex")) %>'
                                                            Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Marital status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvmstatus" runat="server" BackColor="Transparent"
                                                            BorderStyle="None"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mariterialStatus")) %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Permanent Address">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvpreaddress" runat="server" BackColor="Transparent"
                                                            BorderStyle="None"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "peraddress")) %>'
                                                            Width="130px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Post Office">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvpostoffice" runat="server" BackColor="Transparent"
                                                            BorderStyle="None"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postoffice")) %>'
                                                            Width="130px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Police Station">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvpostation" runat="server" BackColor="Transparent"
                                                            BorderStyle="None"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "policestation")) %>'
                                                            Width="130px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Nominee">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvnominee" runat="server" BackColor="Transparent"
                                                            BorderStyle="None"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nominee")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Pick Up Point">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvpickuppoint" runat="server" BackColor="Transparent"
                                                            BorderStyle="None"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pickuppoint")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                                </asp:TemplateField>


                                                


                                                <%--<asp:TemplateField HeaderText="Gross Salary">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvemplist2" runat="server" BackColor="Transparent"
                                                            BorderStyle="None"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salary")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px"></asp:Label>
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFlblgvemplist2" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="16px" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>--%>

                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>  
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



